using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Spreadbot.Core.Mip.Tests
{
    [TestClass]
    public class Test_Mip_Connector
    {
        // ===================================================================================== []
        // Zip_And_Send_Feed
        [TestMethod]
        public void Zip_And_Send_Feed()
        {
            var feed = new Feed(FeedType.Product);
            var response = Connector.SendFeed(feed);

            Trace.TraceInformation("StatusDescription=[{0}]", response.StatusDescription);

            Assert.AreEqual(StatusCode.SendFeedSuccess, response.StatusCode);
            Assert.IsTrue(Request.VerifyRequestId((Request.Identifier)response.Result));
        }

        // ===================================================================================== []
        // Find_Unknown_Request
        [TestMethod]
        public void Find_Unknown_Request()
        {
            var feed = new Feed(FeedType.Product);
            var request = new Request(feed, Request.GenerateId());
            var findResponce = Connector.FindRequest(request, RequestProcessingStage.Inprocess);
            var remoteFileName = (string)findResponce.Result;

            Trace.TraceInformation(findResponce.StatusDescription);

            Assert.AreEqual(StatusCode.FindRequestFail, findResponce.StatusCode);
            Assert.IsNull(remoteFileName);
            Assert.IsTrue(findResponce.StatusDescription.Contains(StatusCode.FindRemoteFileFail.ToString()));
        }

        // ===================================================================================== []
        // Find_Wrong_Request
        [TestMethod]
        public void Find_Wrong_Request()
        {
            var feed = new Feed(FeedType.None);
            var request = new Request(feed, Request.GenerateId());
            var findResponce = Connector.FindRequest(request, RequestProcessingStage.Inprocess);
            var remoteFileName = (string)findResponce.Result;

            Trace.TraceInformation(findResponce.StatusDescription);

            Assert.AreEqual(StatusCode.FindRequestFail, findResponce.StatusCode);
            Assert.IsNull(remoteFileName);
            Assert.IsTrue(findResponce.StatusDescription.Contains("Exception"));
        }

        // ===================================================================================== []
        // Find_Newly_Generated_Request
        [TestMethod]
        public void Find_Newly_Generated_Request()
        {
            // Now: Find_Newly_Generated_Request

            var feed = new Feed(FeedType.Product);
            var sendResponse = Connector.SendFeed(feed);
            var request = new Request(feed, (Request.Identifier)sendResponse.Result);
            var findResponce = Connector.FindRequest(request, RequestProcessingStage.Inprocess);
            var remoteFileName = (string)findResponce.Result;

            Trace.TraceInformation(findResponce.StatusDescription);

            Assert.AreEqual(StatusCode.FindRequestSuccess, findResponce.StatusCode);
            Assert.IsNotNull(remoteFileName);
            Assert.IsTrue(remoteFileName.Length > 1);
        }

        // ===================================================================================== []
        // Check_Request_Status_Output
        [TestMethod]
        public void Check_Request_Status_Output()
        {
            throw new NotImplementedException();
            // Now: Check_Request_Status_Output
            /*
                        var feed = new Feed(FeedType.Product);
                        var response = Connector.CheckRequest();
            */
        }
    }
}