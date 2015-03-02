using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Spreadbot.Core.Mip.Tests
{
    [TestClass]
    public class Test_Connector_Sftp
    {
        // ===================================================================================== []
        [TestMethod]
        public void SendZippedFeed()
        {
            var feed = new Feed(FeedType.Product);
            var response = Connector.SftpHelper.SendZippedFeed(feed.Name, 0.ToString());

            Trace.TraceInformation(response.StatusDescription);
            
            Assert.AreEqual(StatusCode.SendZippedFeedSuccess, response.StatusCode);
        }

        // ===================================================================================== []
        [TestMethod]
        public void TestConnection_Good()
        {
            var response = Connector.SftpHelper.TestConnection();
            
            Trace.TraceInformation(response.StatusDescription);
            
            Assert.AreEqual(StatusCode.TestConnectionSuccess, response.StatusCode);
        }

        // ===================================================================================== []
        [TestMethod]
        public void TestConnection_Bad()
        {
            var response = Connector.SftpHelper.TestConnection("wrong password");

            Trace.TraceInformation(response.StatusDescription);
            
            Assert.AreEqual(StatusCode.TestConnectionFail, response.StatusCode);
        }
    }
}