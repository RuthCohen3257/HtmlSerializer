using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace htmlSerializer
{
    public class HtmlElement
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> Attributes { get; set; }=new List<string>();
        public List<string> Classes { get; set; }= new List<string>();
        public string InnerHtml { get; set; }
        public HtmlElement Parent { get; set; }
        public List<HtmlElement> Children { get; set; } = new List<HtmlElement>();

        public  IEnumerable<HtmlElement> Descendants()
        {
            HtmlElement htmlElement;
            Queue<HtmlElement> queue = new Queue<HtmlElement>();
            queue.Enqueue(this);
            while(queue.Count > 0)
            {
                htmlElement = queue.Dequeue();
                yield return htmlElement;
                if(htmlElement.Children.Count>0)
                {
                    foreach(HtmlElement child in htmlElement.Children)
                    {
                        queue.Enqueue(child);
                    }
                }
            }
        }
        public IEnumerable <HtmlElement> Ancestors()
        {
            HtmlElement htmlElement = this;
            while(htmlElement != null)
            {
                yield return htmlElement;
                htmlElement = htmlElement.Parent;
            }
        }
    }
}
