using MyJeepDeals.Attributes;
using MyJeepDeals.Enums;
using System.Collections.Generic;

namespace MyJeepDeals.SiteDescriptors
{
    public class ExtremeTerrain : ISiteDescriptor
    {
        [Selector(XPath = "//h1[@class='headline product_name']")]
        public string Title { get; set; }

        [Selector(XPath = "//section[@class='description_container']/div[@class='product_description']/text()[not(self::div)]")]
        public string Description { get; set; }

        [Selector(XPath = "//p[@class='price']/span[@class='cost']")]
        public double? Price { get; set; }

        [Selector(XPath = "//div[@class='status']/*[contains(@class,'stock')][not(contains(@class,'hidden'))]//strong", Fallback = "Out of stock")]
        public string Availability { get; set; }

        [Selector(XPath = "//section[@class='full_details']/div[@class='features']/ul")]
        public IList<string> Specs { get; set; }

        [Selector(XPath = "//section[@class='description_container']/div[@class='product_description']", ExtractType = ExtractType.Html)]
        public string Html { get; set; }

        [Selector(XPath = "//div[@class='product_images']/div/div[@class='image_container']//img[@src]", Attribute = "src")]
        public string PrimaryImage { get; set; }
    }
}
