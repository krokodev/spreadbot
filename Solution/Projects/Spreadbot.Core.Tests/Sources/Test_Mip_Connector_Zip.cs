using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Spreadbot.Core.Mip.Tests
{
    [TestClass]
    public class Test_Mip_Connector_Zip
    {
        [TestMethod]
        public void Zip_Feed()
        {
            var feed = new Feed(FeedType.Product);
            var response = Connector.ZipHelper.ZipFeed(feed.Name, 0.ToString());

            Trace.TraceInformation(response.StatusDescription);
            
            Assert.AreEqual(StatusCode.ZipFeedSuccess, response.StatusCode);
        }
    }
}