using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using HtmlAgilityPack;

namespace HTMLprocess
{
    internal class Starter
    {
        public static void Run(string[] htmlDoc)
        {
            var html = File.ReadAllText("E:\\data.txt", Encoding.UTF8);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            List<string> names = new List<string>();

            foreach (HtmlNode vol in doc.DocumentNode.SelectNodes("//tr"))
            {
                string name
                    = vol.ChildNodes[1].InnerText.Trim().Replace(
                        "&nbsp;",
                        string.Empty);

                names.Add(name);

                Console.WriteLine(name);
            }

            File.WriteAllText(
                "E:\\test.txt",
                string.Join(
                    names.ToString(),
                    (char)10));

            Console.ReadKey();
        }
    }
}
