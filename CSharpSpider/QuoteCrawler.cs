using System;
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

    class QuoteCrawler : Crawler
    {
        protected override List<string> ExtractDetailUrls(string listHtml)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(listHtml);

            var authorLinks = doc.DocumentNode.SelectNodes("//span/a");
            var urls = new List<string>();

            if (authorLinks != null)
            {
                foreach (var link in authorLinks)
                {
                    var href = link.GetAttributeValue("href", "");
                    if (!href.Contains("author"))
                        continue;

                    if (!href.StartsWith("http"))
                        href = "https://quotes.toscrape.com" + href + "/";

                    urls.Add(href);
                }
            }

            return urls;
        }

        protected override void ParseDetailPage(string detailHtml)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(detailHtml);

            var name = doc.DocumentNode.SelectSingleNode("//h3")?.InnerText.Trim();
            var desc = doc.DocumentNode.SelectSingleNode("//div[@class='author-description']")?.InnerText.Trim();

            Console.WriteLine($"【作者】{name}");
            Console.WriteLine($"【简介】{desc?.Substring(0, Math.Min(100, desc.Length))}");
        }
    }

}
