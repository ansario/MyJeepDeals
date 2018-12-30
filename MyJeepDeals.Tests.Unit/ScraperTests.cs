using MyJeepDeals;
using NSubstitute;
using NUnit.Framework;
using System.Linq;
using System.Net.Http;

namespace Tests
{
    public class ScraperTests
    {
        private HttpClient _client;
        private IScraper _subject;

        [SetUp]
        public void Setup()
        {
            _client = Substitute.For<HttpClient>();

            _subject = new Scraper(_client);
        }

        [Test]
        [TestCase("<div><h1>test</h1></div>", "//div/h1", "", "", "test")]
        [TestCase("<div><h1></h1></div>", "//div/h1", "fallback", "", "fallback")]
        [TestCase("<div><h1>test1</h1></div>", "//div/h1", "fallback", "test(.*)", "1")]
        public void CanScrapeString(string pageContents, string xPath, string fallBack, string regex, string expected)
        {
            var test = _subject.ScrapeString(pageContents, xPath, fallBack, regex);
          
            Assert.True(test == expected);
        }

        [Test]
        [TestCase("<div><h1>32.4insertedtext</h1></div>", "//div/h1", "(.*)insertedtext", 32.4)]
        [TestCase("<div><h1></h1></div>", "//div/h1", "", null)]
        [TestCase("<div><h1>32.4</h1></div>", "//div/h1", "", 32.4)]
        public void CanScrapeDouble(string pageContents, string xPath, string regex, double? expected)
        {
            var test = _subject.ScrapeDouble(pageContents, xPath, regex);

            Assert.True(test == expected);
        }

        [Test]
        [TestCase("<div><ul><li>test1</li><li>test2</li></ul></div>", "//div/ul", "test1,test2" )]
        [TestCase("<div><ul><li>test1</li><li>test2</li><li>non</li></ul></div>", "//div/ul", "test1,test2,non")]
        public void CanScrapeList(string pageContents, string xPath, string expected)
        {
            var test = _subject.ScrapeList(pageContents, xPath);
            Assert.True(test.ToArray().SequenceEqual(expected.Split(',')));
        }
    }
}