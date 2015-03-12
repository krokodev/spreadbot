using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Spreadbot.Core.Channel.Ebay.Mip.Tests
{
    [TestClass]
    public class MipConnector_SftpHelper_Tests
    {
        // ===================================================================================== []
        [TestMethod]
        public void SendZippedFeed()
        {
            var feed = new MipFeed(MipFeedType.Product);
            var response = MipConnector.SftpHelper.SendZippedFeed(feed.Name, MipRequest.GenerateTestId());

            Trace.TraceInformation(response.Autoinfo);
            
            Assert.AreEqual(MipStatusCode.SendZippedFeedSuccess, response.Code);
        }

        // ===================================================================================== []
        [TestMethod]
        public void TestConnection_Good()
        {
            var response = MipConnector.SftpHelper.TestConnection();
            
            Trace.TraceInformation(response.Autoinfo);
            
            Assert.AreEqual(MipStatusCode.TestConnectionSuccess, response.Code);
        }

        // ===================================================================================== []
        [TestMethod]
        public void TestConnection_Bad()
        {
            var response = MipConnector.SftpHelper.TestConnection("wrong password");

            Trace.TraceInformation(response.Autoinfo);
            
            Assert.AreEqual(MipStatusCode.TestConnectionFail, response.Code);
        }
    }
}