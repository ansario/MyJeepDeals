using System.Collections.Generic;

namespace MyJeepDeals.SiteDescriptors
{
    public interface ISiteDescriptor
    {
        string Title { get; set; }
        string Description { get; set; }
        double? Price { get; set; }
        string Availability { get; set; }
        IList<string> Specs { get; set; }
        string Html { get; set; }
        string PrimaryImage { get; set; }
    }
}