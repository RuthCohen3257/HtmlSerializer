using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace htmlSerializer
{
    public static class ExtensionsFunctions
    {

    public static IEnumerable<HtmlElement> FindElementsBySelector(this HtmlElement element, Selector selector)
        {
            var results = new HashSet<HtmlElement>();
            FindElementsBySelectorRecursive(element, selector, results);
            return results;
        }

        private static void FindElementsBySelectorRecursive(HtmlElement element, Selector selector, HashSet<HtmlElement> results)
        {
            IEnumerable<HtmlElement> f = element.Descendants();
            if (selector.Child == null)
            {
                results.Add(element);
                return;
            }

            List<HtmlElement> r = new List<HtmlElement>();
            foreach (HtmlElement e in f)
            {
                if (MatchesSelector(e, selector))
                    r.Add(e);
            }
            foreach (HtmlElement child in r)
            {
                FindElementsBySelectorRecursive(child, selector.Child, results);
            }
        }

        private static bool MatchesSelector(HtmlElement htmlElement2, Selector selector)
        {
            return
            (string.IsNullOrEmpty(selector.TagName) || htmlElement2.Name == selector.TagName) &&
            (string.IsNullOrEmpty(selector.Id) || htmlElement2.Id == selector.Id) &&
            (selector.Classes.All(cls => htmlElement2.Classes.Contains(cls)));
        }
    }
}

