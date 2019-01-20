using MyJeepDeals.Attributes;
using MyJeepDeals.Enums;
using System.Collections.Generic;

namespace MyJeepDeals.SiteDescriptors
{
    public class FourWD : ISiteDescriptor
    { 
        [Selector(XPath = "//*[@itemprop='name']", Attribute = "content")]
        public string Title { get; set; }

        [Selector(XPath = "//*[@itemprop='description']", Attribute = "content")]
        public string Description { get; set; }

        [Selector(XPath = "//*[@itemprop='price']", Attribute = "content")]
        public double? Price { get; set; }

        [Selector(XPath = "//*[@itemprop='availability']", Fallback = "Out of stock", Attribute = "content")]
        public string Availability { get; set; }

        [Selector(XPath = "//*[@id='specsSection']/ul")]
        public IList<string> Specs { get; set; }

        [Selector(XPath = "//*[@id='specsSection']", ExtractType = ExtractType.Html)]
        public string Html { get; set; }

        [Selector(XPath = "//img[@class='img-responsive zoomable hidden']", Attribute = "data-zoom-image")]
        public string PrimaryImage { get; set; }
    }
}
