using System.Diagnostics;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Spreadbot.Core.Mip.Tests
{
    [TestClass]
    public class Test_Mip_Connector_Zip
    {
        // ===================================================================================== []
        // Zip_Feed
        [TestMethod]
        public void Zip_Feed()
        {
            var feed = new Feed(FeedType.Product);
            var reqId = 0.ToString();
            var response = Connector.ZipHelper.ZipFeed(feed.Name, reqId);

            Trace.TraceInformation(response.StatusDescription);

            Assert.AreEqual(StatusCode.ZipFeedSuccess, response.StatusCode);
            Assert.IsTrue(File.Exists(Connector.ZipHelper.ZippedFeedFileName(feed.Name, reqId)));
            Assert.AreEqual(StatusCode.ZipFeedSuccess, response.StatusCode);
        }
    }
}