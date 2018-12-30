using System.Collections.Generic;

namespace MyJeepDeals.Models
{
    public class ScrapedResult
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double? Price { get; set; }
        public string Availability { get; set; }
        public IList<string> Specs { get; set; } = new List<string>();
        public string Html { get; set; }
        public string PrimaryImage { get; set; }

        public object this[string propertyName]
        {

            get { return this.GetType().GetProperty(propertyName)?.GetValue(this, null); }

            set { this.GetType().GetProperty(propertyName)?.SetValue(this, value, null); }

        }
    }
}
