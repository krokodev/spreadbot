using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Spreadbot.Core.Mip.Tests
{
    [TestClass]
    public class MipConnector
    {
        [TestMethod]
        public void Upload_Feed_On_MIP()
        {
            var feed = new MipFeed(MipFeedType.Product);
            var settings = new MipSettings();
            var mip = new Mip.MipConnector(settings);
            var response = mip.UploadFeed(feed);

            Assert.AreEqual(MipStatusCode.CommandOk, response.StatusCode);
        }

        [TestMethod]
        public void WinSCP_Works_And_Returns_Authentication_Failed()
        {
            try
            {
                var sessionOptions = new WinSCP.SessionOptions
                {
                    Protocol = WinSCP.Protocol.Sftp,
                    HostName = "mip.ebay.com",
                    PortNumber = 22,
                    UserName = "admin",
                    GiveUpSecurityAndAcceptAnySshHostKey = true
                };

                using (var session = new WinSCP.Session())
                {
                    session.Open(sessionOptions);
                }
            }
            catch (WinSCP.SessionRemoteException e)
            {
                Trace.TraceInformation(e.InnerException.Message);

                Assert.AreEqual(
                    true,
                    e.InnerException.Message.Contains("Authentication failed"),
                    "WinSCP.SessionRemoteException must contain: [Authentication failed]");
            }
        }
    }
}