using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WinSCP;

namespace Spreadbot.Core.Mip.Tests
{
    [TestClass]
    public class MipConnector_Sftp_Tests
    {
        [TestMethod]
        public void Send_Zipped_Feed_To_MIP()
        {
            var feed = new MipFeed(MipFeedType.Product);
            var response = MipConnector.SftpHelper.SendZippedFeed(feed, (MipRequest.Identifier)1000);

            Trace.TraceInformation(response.StatusDescription);
            Assert.AreEqual(MipStatusCode.FeedUploaded, response.StatusCode);
        }

/*
        [TestMethod]
        public void Zip_And_Send_Feed_To_MIP()
        {
            var feed = new MipFeed(MipFeedType.Product);
            var response = MipConnector.SendFeed(feed);

            Trace.TraceInformation("response.RequestId={0}", response.RequestId);
            
            Assert.AreEqual(MipStatusCode.FeedUploaded, response.StatusCode);
            Assert.IsTrue(true, MipRequest.VerifyRequestId(response.RequestId));
        }
 */

        [TestMethod]
        public void Test_Good_Connection()
        {
            var response = MipConnector.SftpHelper.TestConnection();

            Assert.AreEqual(MipStatusCode.ConnectionOk, response.StatusCode);
        }

        [TestMethod]
        public void Test_Bad_Connection()
        {
            var response = MipConnector.SftpHelper.TestConnection("wrong password");

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

                Assert.IsTrue(
                    e.InnerException.Message.Contains("Authentication failed"),
                    "WinSCP.SessionRemoteException must contain: [Authentication failed]");
            }
        }
    }
}