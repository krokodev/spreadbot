using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WinSCP;

namespace Spreadbot.Core.Mip.Tests
{
    [TestClass]
    public class Test_Mip_Connector_Sftp
    {
        [TestMethod]
        public void Send_Zipped_Feed_To_MIP()
        {
            var feed = new Feed(FeedType.Product);
            var response = Connector.SftpHelper.SendZippedFeed(feed.Name, 0.ToString());

            Trace.TraceInformation(response.StatusDescription);
            Assert.AreEqual(StatusCode.FeedSent, response.StatusCode);
        }

        [TestMethod]
        public void Test_Good_Connection()
        {
            var response = Connector.SftpHelper.TestConnection();

            Assert.AreEqual(StatusCode.ConnectionOk, response.StatusCode);
        }

        [TestMethod]
        public void Test_Bad_Connection()
        {
            var response = Connector.SftpHelper.TestConnection("wrong password");

            Assert.AreEqual(StatusCode.Error, response.StatusCode);
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

                Assert.IsTrue(
                    e.InnerException.Message.Contains("Authentication failed"),
                    "WinSCP.SessionRemoteException must contain: [Authentication failed]");
            }
        }
    }
}