using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WinSCP;

namespace Spreadbot.Core.Mip.Tests
{
    [TestClass]
    public class Test_Mip_Connector_Sftp
    {
        // ===================================================================================== []
        // Send_Zipped_Feed_To_MIP
        [TestMethod]
        public void Send_Zipped_Feed_To_MIP()
        {
            var feed = new Feed(FeedType.Product);
            var response = Connector.SftpHelper.SendZippedFeed(feed.Name, 0.ToString());

            Trace.TraceInformation(response.StatusDescription);
            Assert.AreEqual(StatusCode.SendZippedFeedSuccess, response.StatusCode);
        }

        // ===================================================================================== []
        // Test_Good_Connection
        [TestMethod]
        public void Test_Good_Connection()
        {
            var response = Connector.SftpHelper.TestConnection();

            Assert.AreEqual(StatusCode.TestConnectionSuccess, response.StatusCode);
        }

        // ===================================================================================== []
        // Test_Bad_Connection
        [TestMethod]
        public void Test_Bad_Connection()
        {
            var response = Connector.SftpHelper.TestConnection("wrong password");

            Assert.AreEqual(StatusCode.TestConnectionFail, response.StatusCode);
        }
    }
}