using HtmlAgilityPack;
using MyJeepDeals.Attributes;
using MyJeepDeals.Enums;
using MyJeepDeals.Models;
using MyJeepDeals.SiteDescriptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyJeepDeals
{
    public class Scraper : IScraper
    {
        private readonly HttpClient _client;

        public Scraper(HttpClient client)
        {
            _client = client;
        }

        public string ScrapeString(string pageContents, string xPath, string fallback = "", string regex = "", ExtractType extractType = ExtractType.Text, string attribute = "")
        {
            var result = "";

            var pageDocument = new HtmlDocument();
            pageDocument.LoadHtml(pageContents);

            var nodes = pageDocument.DocumentNode.SelectNodes(xPath);

            if(nodes == null)
            {
                return !string.IsNullOrWhiteSpace(result) ? result : fallback;
            }

            foreach (var node in nodes)
            {
                if (!string.IsNullOrWhiteSpace(regex))
                {
                    var match = Regex.Match(extractType == ExtractType.Text ? node.InnerText : node.InnerHtml, regex);

                    foreach (var m in match.Groups.Skip(1))
                    {
                        result += m.Value;
                    }
                }
                else if(!string.IsNullOrWhiteSpace(attribute))
                {
                    result += node.Attributes.FirstOrDefault(x => x.Name == attribute).Value;
                }
                else
                {
                    result += extractType == ExtractType.Text ? node.InnerText : node.InnerHtml;
                }
            }

            return !string.IsNullOrWhiteSpace(result) ? result : fallback;
        }

        public double? ScrapeDouble(string pageContents, string xPath, string regex = "")
        {
            double? result = null;

            var pageDocument = new HtmlDocument();
            pageDocument.LoadHtml(pageContents);

            var nodes = pageDocument.DocumentNode.SelectNodes(xPath);

            if (nodes == null)
            {
                return result;
            }

            foreach (var node in nodes)
            {
                if (!string.IsNullOrWhiteSpace(regex))
                {
                    var match = Regex.Match(node.InnerText, regex);

                    foreach (var m in match.Groups.Skip(1))
                    {
                        result = Convert.ToDouble(GetNumbers(m.ToString()));
                    }
                }
                else
                {
                    if(!string.IsNullOrWhiteSpace(node.InnerText))
                    {
                        result = Convert.ToDouble(GetNumbers(node.InnerText));
                    }
                }
            }

            return result;
        }

        public IList<string> ScrapeList(string pageContents, string xPath)
        {
            List<string> results = new List<string>();

            var pageDocument = new HtmlDocument();
            pageDocument.LoadHtml(pageContents);

            var nodes = pageDocument.DocumentNode.SelectNodes(xPath);

            if (nodes == null)
            {
                return results;
            }

            foreach (var node in nodes)
            {             
                results.AddRange(node.ChildNodes.Where(x => x.ChildNodes.Count > 0).Select(x => x.InnerText));
            }

            return results;
        }

        public async Task<ScrapedResult> Scrape(string url, ISiteDescriptor siteDescriptor)
        {
            var response = await _client.GetAsync(url);
            var pageContents = await response.Content.ReadAsStringAsync();
            var properties = siteDescriptor.GetType().GetProperties();

            var result = new ScrapedResult()
            {
                Url = url,
                Title = ScrapeString(pageContents, ((Selector)properties.FirstOrDefault(x => x.Name == "Title").GetCustomAttribute(typeof(Selector))).XPath),
                Description = ScrapeString(pageContents, ((Selector)properties.FirstOrDefault(x => x.Name == "Description").GetCustomAttribute(typeof(Selector))).XPath),
                Price = ScrapeDouble(pageContents, ((Selector)properties.FirstOrDefault(x => x.Name == "Price").GetCustomAttribute(typeof(Selector))).XPath, ((Selector)properties.FirstOrDefault(x => x.Name == "Price").GetCustomAttribute(typeof(Selector))).Regex),
                Availability = ScrapeString(pageContents, ((Selector)properties.FirstOrDefault(x => x.Name == "Availability").GetCustomAttribute(typeof(Selector))).XPath, "Out of Stock"),
                Specs = ScrapeList(pageContents, ((Selector)properties.FirstOrDefault(x => x.Name == "Specs").GetCustomAttribute(typeof(Selector))).XPath),
                Html = ScrapeString(pageContents, ((Selector)properties.FirstOrDefault(x => x.Name == "Html").GetCustomAttribute(typeof(Selector))).XPath, "", ((Selector)properties.FirstOrDefault(x => x.Name == "Html").GetCustomAttribute(typeof(Selector))).Regex, ExtractType.Html),
                PrimaryImage = ScrapeString(pageContents, ((Selector)properties.FirstOrDefault(x => x.Name == "PrimaryImage").GetCustomAttribute(typeof(Selector))).XPath, "", ((Selector)properties.FirstOrDefault(x => x.Name == "PrimaryImage").GetCustomAttribute(typeof(Selector))).Regex)
            };

            result.PrimaryImage = ScrapeString(pageContents, ((Selector)properties.FirstOrDefault(x => x.Name == "PrimaryImage").GetCustomAttribute(typeof(Selector))).XPath, "", ((Selector)properties.FirstOrDefault(x => x.Name == "PrimaryImage").GetCustomAttribute(typeof(Selector))).Regex, ExtractType.Text, ((Selector)properties.FirstOrDefault(x => x.Name == "PrimaryImage").GetCustomAttribute(typeof(Selector))).Attribute);


            return result;
        }

        private static string GetNumbers(string input)
        {
            return Regex.Replace(input, "[^.0-9]", "");
        }
    }
}
