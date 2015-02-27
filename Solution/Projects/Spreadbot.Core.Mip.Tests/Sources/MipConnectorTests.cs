using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WinSCP;

namespace Spreadbot.Core.Mip.Tests
{
    // >> Now: Mip.Tests

    [TestClass]
    public class MipConnectorTests
    {
        [TestMethod]
        public void Upload_Zipped_Feed_To_MIP()
        {
            var feed = new MipFeed(MipFeedType.Product);
            var mip = new MipConnector();
            var response = mip.SendZippedFeed(feed, (MipRequest.Identifier)1234);

            Trace.TraceInformation(response.StatusDescription);
            Assert.AreEqual(MipStatusCode.FeedUploaded, response.StatusCode);
        }

        [TestMethod]
        public void Test_Good_Connection()
        {
            var feed = new MipFeed(MipFeedType.Product);
            var mip = new MipConnector();
            var response = mip.TestConnection();

            Assert.AreEqual(MipStatusCode.ConnectionOk, response.StatusCode);
        }

        [TestMethod]
        public void Test_Bad_Connection()
        {
            var feed = new MipFeed(MipFeedType.Product);
            var mip = new MipConnector();
            var response = mip.TestConnection("wrong password");

            Assert.AreEqual(MipStatusCode.Error, response.StatusCode);
        }

        [TestMethod]
        public void WinSCP_Works_And_Returns_Authentication_Failed()
        {
            try
            {
                var sessionOptions = new SessionOptions
                {
                    Protocol = Protocol.Sftp,
                    HostName = "mip.ebay.com",
                    PortNumber = 22,
                    UserName = "admin",
                    GiveUpSecurityAndAcceptAnySshHostKey = true
                };

                using (var session = new Session())
                {
                    session.Open(sessionOptions);
                }
            }
            catch (SessionRemoteException e)
            {
                Trace.TraceInformation(e.InnerException.Message);

                Assert.AreEqual(
                    true,
                    e.InnerException.Message.Contains("Authentication failed"),
                    "WinSCP.SessionRemoteException must contain: [Authentication failed]");
            }
        }

        [TestMethod]
        public void Read_Mip_Config()
        {
            var configuration = MipConfiguration.Instance;
            Assert.AreEqual("mip.ebay.com", configuration.Connection.HostName);
            Assert.AreEqual(22, configuration.Connection.PortNumber);
        }

        [TestMethod]
        public void Read_Mip_Security_Config()
        {
            var configuration = MipSecurityConfiguration.Instance;
            Assert.AreEqual("cyfir", configuration.SecretData.UserName);
        }
    }
}