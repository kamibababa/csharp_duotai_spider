using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSpider
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;

    abstract class Crawler
    {
        protected readonly HttpClient _httpClient = new HttpClient();

        public async Task StartAsync(string listUrl)
        {
            Console.WriteLine($"\n=== 抓取列表页：{listUrl} ===");
            string listHtml = await _httpClient.GetStringAsync(listUrl);

            var detailUrls = ExtractDetailUrls(listHtml);
            Console.WriteLine($"共提取到 {detailUrls.Count} 个详情页链接");

            foreach (var url in detailUrls)
            {
                Console.WriteLine($"\n-- 抓取详情页：{url}");
                string detailHtml = await _httpClient.GetStringAsync(url);
                ParseDetailPage(detailHtml);
            }
        }

        protected abstract List<string> ExtractDetailUrls(string listHtml);
        protected abstract void ParseDetailPage(string detailHtml);
    }

}
