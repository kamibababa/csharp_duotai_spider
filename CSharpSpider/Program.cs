namespace CSharpSpider
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Crawler books = new BookCrawler();
            await books.StartAsync("https://books.toscrape.com/");
            Crawler quotes = new QuoteCrawler();
            await quotes.StartAsync("https://quotes.toscrape.com/");

    

     
        }
    }
}
