using MyJeepDeals.Enums;
using System;

namespace MyJeepDeals.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    internal class Selector : Attribute
    {
        public virtual string XPath { get; set; }
        public virtual string Fallback { get; set; }
        public virtual string Regex { get; set; }
        public virtual ExtractType ExtractType { get; set; } = ExtractType.Text;
        public virtual string Attribute { get; set; }
    }
}
