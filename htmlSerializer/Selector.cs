using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace htmlSerializer
{
    public class Selector
    {

        public string TagName { get; set; }
        public string Id { get; set; }
        public List<string> Classes { get; set; }=new List<string>();
        public Selector Parent { get; set; }
        public Selector Child { get; set; }

        public static Selector BuildSel(string query)
        {
            bool isFirst = false;
            List<string> sList = query.Split(" ").ToList();
            Selector root = new Selector();
            Selector currentSelector = root;
            string tag = "";
            foreach (string s in sList)
            {
                tag = "";
                for (int i = 0; i < s.Length; i++)
                {
                    if (s[i] != '#' && s[i] != '.')
                    {
                        if (i == 0)//שם התג ודאי יהיה הראשון  
                        {
                            int index = i;
                            while (index < s.Length && (s[index] != '#' && s[index] != '.'))
                            {
                                tag += s[index++];
                            }
                            if (HtmlHelper.Instance.LablesWithClosure.Contains(tag) ||
                              HtmlHelper.Instance.LablesWithoutClosure.Contains(tag))
                            {
                                if (!isFirst)
                                {
                                    root = currentSelector;
                                    isFirst = true;
                                }
                                currentSelector.TagName = tag;
                                
                            }
                            i = --index;
                        }
                    }
                    else
                    {
                        if (s[i] == '#')
                        {
                            if (!isFirst)
                            {
                                root = currentSelector;
                                isFirst = true;
                            }
                            currentSelector.Id = FindId(s.Substring(i + 1));
                        }
                        else if (s[i] == '.')
                        {
                            if (!isFirst)
                            {
                                root = currentSelector;
                                isFirst = true;
                            }
                            currentSelector.Classes.Add(FindClass(s.Substring(i + 1)));
                        }
                    }

                }
                Selector selector = new Selector();
                currentSelector.Child = selector;
                selector.Parent = currentSelector;
                currentSelector = selector;
            }
            return root;
        }

        public static string FindId(string str)
        {
            string result = "";
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '#' || str[i] == '.')
                    break;
                result += str[i];
            }
            return result;
        }

        public static string FindClass(string str)
        {
            string result = "";
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '#' || str[i] == '.')
                    break;
                result += str[i];
            }
            return result;
        }


       
    }
}

