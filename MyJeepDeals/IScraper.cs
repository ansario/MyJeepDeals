using MyJeepDeals.Enums;
using MyJeepDeals.Models;
using MyJeepDeals.SiteDescriptors;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyJeepDeals
{
    public interface IScraper
    {
        string ScrapeString(string pageContents, string xPath, string fallback = "", string regex = "", ExtractType extractType = ExtractType.Text, string attribute = "");
        double? ScrapeDouble(string pageContents, string xPath, string regex = "", string attribute = "");
        IList<string> ScrapeList(string pageContents, string xPath);
        Task<ScrapedResult> Scrape(string url, ISiteDescriptor siteDescriptor);
    }
}