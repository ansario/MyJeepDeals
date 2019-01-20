using MyJeepDeals.SiteDescriptors;
using Newtonsoft.Json;
using System;
using System.Net.Http;

namespace MyJeepDeals.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            var scraper = new Scraper(new HttpClient());

            var results = scraper.Scrape("https://www.extremeterrain.com/smittybilt-beaver-2-receiver-hitch-step.html", new ExtremeTerrain()).Result;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Extreme Terrain Results: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(JsonConvert.SerializeObject(results));

            results = scraper.Scrape("https://www.quadratec.com/p/quadratec/winch-ready-bull-bar-front-bumpers-97-06-jeep-wrangler-tj-unlimited", new Quadratec()).Result;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Quadratec Results: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(JsonConvert.SerializeObject(results));

            var results = scraper.Scrape("https://www.4wd.com/p/poison-spyder-customs-jl-crawler-front-bumper-19-58-010dbp1/_/R-FVNN-19-58-010DBP1", new FourWD()).Result;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"4WD Results: ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(JsonConvert.SerializeObject(results));
            Console.ReadLine();
        }
    }
}
