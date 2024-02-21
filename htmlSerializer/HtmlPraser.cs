using System.Linq;
using System.Text.RegularExpressions;
namespace htmlSerializer
{

    public class HtmlParser
    {
        private HtmlHelper htmlHelper;

        public HtmlParser()
        {
            htmlHelper = HtmlHelper.Instance;
        }

        public HtmlElement Serialize(string html)
        {
            var cleanHtml = new Regex("\\s+").Replace(html, " ");
            var htmlLines = new Regex("<(.*?)>").Split(cleanHtml).Where(s => s.Length > 0).ToList();
            var root = new HtmlElement();
            var currentElement = root;

            foreach (var line in htmlLines)
            {
                var firstWord = line.Split(' ')[0];

                if (firstWord == "/html")
                {
                    break; // Reached end of HTML
                }
                else if (firstWord.StartsWith("/"))
                {
                    if (currentElement.Parent != null) // Make sure there is a valid parent
                    {
                        currentElement = currentElement.Parent; // Go to previous level in the tree
                    }
                }
                else if (HtmlHelper.Instance.LablesWithClosure.Contains(firstWord))
                {
                    var newElement = new HtmlElement();
                    newElement.Name = firstWord;

                    // Handle attributes
                    var restOfString = line.Remove(0, firstWord.Length);
                    var attributes = Regex.Matches(restOfString, "([a-zA-Z]+)=\\\"([^\\\"]*)\\\"")
                        .Cast<Match>()
                        .Select(m => $"{m.Groups[1].Value}=\"{m.Groups[2].Value}\"")
                        .ToList();

                    if (attributes.Any(attr => attr.StartsWith("class")))
                    {
                        // Handle class attribute
                        var classAttr = attributes.First(attr => attr.StartsWith("class"));
                        var classes = classAttr.Split('=')[1].Trim('"').Split(' ');
                        newElement.Classes.AddRange(classes);
                    }

                    newElement.Attributes.AddRange(attributes);

                    // Handle ID
                    var idAttribute = attributes.FirstOrDefault(attr => attr.StartsWith("id"));
                    if (!string.IsNullOrEmpty(idAttribute))
                    {
                        newElement.Id = idAttribute.Split('=')[1].Trim('"');
                    }

                    newElement.Parent = currentElement;
                    currentElement.Children.Add(newElement);

                    // Check if self-closing tag
                    if (line.EndsWith("/") || HtmlHelper.Instance.LablesWithoutClosure.Contains(firstWord))
                    {
                        currentElement = newElement.Parent;
                    }
                    else
                    {
                        currentElement = newElement;
                    }
                }
                else
                {
                    // Text content
                    currentElement.InnerHtml = line;
                }
            }

            return root;
        }
    }




}
