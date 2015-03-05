using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Spreadbot.Core.Mip.Tests
{
    [TestClass]
    public class Test_Connector_SftpHelper
    {
        // ===================================================================================== []
        [TestMethod]
        public void SendZippedFeed()
        {
            var feed = new Feed(FeedType.Product);
            var response = Connector.SftpHelper.SendZippedFeed(feed.Name, Request.GenerateTestId());

            Trace.TraceInformation(response.Description);
            
            Assert.AreEqual(StatusCode.SendZippedFeedSuccess, response.Code);
        }

        // ===================================================================================== []
        [TestMethod]
        public void TestConnection_Good()
        {
            var response = Connector.SftpHelper.TestConnection();
            
            Trace.TraceInformation(response.Description);
            
            Assert.AreEqual(StatusCode.TestConnectionSuccess, response.Code);
        }

        // ===================================================================================== []
        [TestMethod]
        public void TestConnection_Bad()
        {
            var response = Connector.SftpHelper.TestConnection("wrong password");

            Trace.TraceInformation(response.Description);
            
            Assert.AreEqual(StatusCode.TestConnectionFail, response.Code);
        }
    }
}