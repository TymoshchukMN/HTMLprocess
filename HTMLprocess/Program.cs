using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using HtmlAgilityPack;

namespace HTMLprocess
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var html = File.ReadAllText("E:\\data.txt", Encoding.UTF8);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            List <string> names = new List<string>();

            foreach (HtmlNode vol in doc.DocumentNode.SelectNodes("//tr"))
            {
                string name = vol.ChildNodes[1].InnerText.Trim().Replace("&nbsp;", string.Empty);
                names.Add(name);
                Console.WriteLine(name);
            }

            Console.ReadKey();
        }

    }
}
