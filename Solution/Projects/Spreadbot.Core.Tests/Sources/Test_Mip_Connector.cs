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

            Trace.TraceInformation("StatusDescription=[{0}]", response.StatusDescription);

            Assert.AreEqual(StatusCode.SendFeedSuccess, response.StatusCode);
            Assert.IsTrue(Request.VerifyRequestId(response.RequestId));
        }
    }
}