using System.Diagnostics;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Spreadbot.Core.Mip.Tests
{
    [TestClass]
    public class Test_Connector_ZipHelper
    {
        // ===================================================================================== []
        [TestMethod]
        public void ZipFeed()
        {
            var feed = new Feed(FeedType.Product);
            var reqId = 0.ToString();
            var response = Connector.ZipHelper.ZipFeed(feed.Name, reqId);

            Trace.TraceInformation(response.Description);

            Assert.AreEqual(StatusCode.ZipFeedSuccess, response.Code);
            Assert.IsTrue(File.Exists(Connector.ZipHelper.ZippedFeedFileName(feed.Name, reqId)));
            Assert.AreEqual(StatusCode.ZipFeedSuccess, response.Code);
        }
    }
}