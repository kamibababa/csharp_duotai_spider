﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSpider
{
    using System;
    using System.Collections.Generic;
    using System.Xml;
    using HtmlAgilityPack;
    using static System.Collections.Specialized.BitVector32;

    class BookCrawler : Crawler
    {
        protected override List<string> ExtractDetailUrls(string listHtml)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(listHtml);

            var bookLinks = doc.DocumentNode.SelectNodes("//h3/a");
            var urls = new List<string>();

            if (bookLinks != null)
            {
                foreach (var link in bookLinks)
                {
                    var href = link.GetAttributeValue("href", "");
                    //href = href.Replace("../", "");
                    urls.Add("https://books.toscrape.com/" + href);
                }
            }

            return urls;
        }

        protected override void ParseDetailPage(string detailHtml)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(detailHtml);

            var title = doc.DocumentNode.SelectSingleNode("/html/body/div[1]/div/div[2]/div[2]/article/div[1]/div[2]/h1")?.InnerText.Trim();
            var price = doc.DocumentNode.SelectSingleNode("/html/body/div[1]/div/div[2]/div[2]/article/div[1]/div[2]/p[1]")?.InnerText.Trim();
            //var desc = doc.DocumentNode.SelectSingleNode("//meta[@name='description']")?.GetAttributeValue("content", "").Trim();

            Console.WriteLine($"【书名】{title}");
            Console.WriteLine($"【价格】{price}");
            //Console.WriteLine($"【简介】{desc?.Substring(0, Math.Min(100, desc.Length))}");
        }
    }

}
