//////////////////////////////////////////
// Author : Tymoshchuk Maksym
// Created On : 09/10/2023
// Last Modified On :
// Description: Process HTML doc
// Project: HTMLprocess
//////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using HtmlAgilityPack;

namespace HTMLprocess
{
    public class HendlerHTML : IHendlerHTML
    {
        private const string PathHTMLfile = "E:\\messageBody.txt";
        private readonly string _html;

        public HendlerHTML()
        {
            _html = File.ReadAllText(PathHTMLfile, Encoding.UTF8);
            if (_html.Length == 0)
            {
                throw new Exception("File messageBody.txt is Empty");
            }
        }

        public List<string> ProccessHTML()
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(_html);

            List<string> names = new List<string>();

            foreach (HtmlNode vol in doc.DocumentNode.SelectNodes("//tr"))
            {
                string name
                    = vol.ChildNodes[1].InnerText.Trim().Replace(
                        "&nbsp;",
                        string.Empty);

                names.Add(name);
            }

            return names;
        }
    }
}
