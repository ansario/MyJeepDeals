using MyJeepDeals.Attributes;
using MyJeepDeals.Enums;
using System.Collections.Generic;

namespace MyJeepDeals.SiteDescriptors
{
    public class Quadratec : ISiteDescriptor
    {
        [Selector(XPath = "//div[@class='row branding']/div/h1")]
        public string Title { get; set; }

        [Selector(XPath = "//div[@class='product-section description']/div[@class='section-content'][not(self::a)]")]
        public string Description { get; set; }

        [Selector(XPath = "(//script[contains(.,'price')])[last()]", Regex = "\\\"price\\\":.*?\"(\\d.*?)\\\"")]
        public double? Price { get; set; }

        [Selector(XPath = "//div[@class='pricing']/div[@class='stock-status stock-status-instock']", Fallback = "Out of stock")]
        public string Availability { get; set; }

        [Selector(XPath = "//div[@id='product-specs']/ul")]
        public IList<string> Specs { get; set; }

        [Selector(XPath = "//div[@class='product-section description']/div[@class='section-content']", ExtractType = ExtractType.Html)]
        public string Html { get; set; }

        [Selector(XPath = "(//script[contains(.,'image')])[2]", Regex = "\\\"image\\\":.*?\"(.*?)\\\"")]
        public string PrimaryImage { get; set; }
    }
}
