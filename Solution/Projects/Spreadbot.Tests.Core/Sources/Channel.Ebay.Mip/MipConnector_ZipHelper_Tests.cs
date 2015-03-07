using System.Diagnostics;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Spreadbot.Core.Channel.Ebay.Mip.Tests
{
    [TestClass]
    public class MipConnector_ZipHelper_Tests
    {
        // ===================================================================================== []
        [TestMethod]
        public void ZipFeed()
        {
            var feed = new Feed(FeedType.Product);
            var reqId = Request.GenerateTestId().ToString();

            var response = Connector.ZipHelper.ZipFeed(feed.Name, reqId);
            Trace.TraceInformation(response.Description);

            Assert.AreEqual(StatusCode.ZipFeedSuccess, response.Code);
            Assert.IsTrue(File.Exists(response.Result.ZipFileName));
            Assert.AreEqual(StatusCode.ZipFeedSuccess, response.Code);
        }
    }
}