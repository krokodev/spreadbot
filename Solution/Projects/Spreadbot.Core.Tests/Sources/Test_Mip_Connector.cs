using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Spreadbot.Core.Mip.Tests
{
    [TestClass]
    public class Test_Mip_Connector
    {
        [TestMethod]
        public void Zip_And_Send_Feed_To_MIP()
        {
            var feed = new Feed(FeedType.Product);
            var response = Connector.SendFeed(feed);
            Trace.TraceInformation("response.RequestId=[{0}]", response.RequestId);

            Assert.AreEqual(StatusCode.SendFeedOk, response.StatusCode);
            Assert.IsTrue(Request.VerifyRequestId(response.RequestId));
        }
    }
}