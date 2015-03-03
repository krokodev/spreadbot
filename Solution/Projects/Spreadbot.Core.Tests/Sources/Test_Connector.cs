using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Spreadbot.Core.Mip.Tests
{
    [TestClass]
    public class Test_Connector
    {
        // ===================================================================================== []
        // SendFeed
        [TestMethod]
        public void SendFeed()
        {
            var feed = new Feed(FeedType.Product);
            var response = Connector.SendFeed(feed);

            Trace.TraceInformation(response.StatusDescription);

            Assert.AreEqual(StatusCode.SendFeedSuccess, response.StatusCode);
            Assert.IsTrue(Request.VerifyRequestId((Request.Identifier) response.Result));
        }

        // ===================================================================================== []
        // FindRequest
        [TestMethod]
        public void FindRequest_Inprocess()
        {
            var feed = new Feed(FeedType.Product);
            var sendResponse = Connector.SendFeed(feed);
            var request = new Request(feed, (Request.Identifier) sendResponse.Result);
            var findResponce = Connector.FindRequest(request, RequestProcessingStage.Inprocess);
            var findResult = (FindRemoteFileResult)findResponce.Result;

            Trace.TraceInformation(findResponce.StatusDescription);

            Assert.AreEqual(StatusCode.FindRequestSuccess, findResponce.StatusCode);
            Assert.IsNotNull(findResult.FileName);
            Assert.IsNotNull(findResult.FolderPath);
            Assert.IsTrue(findResult.FileName.Length > 1);
        }

        // --------------------------------------------------------[]
        [TestMethod]
        public void FindRequest_Inprocess_Unknown()
        {
            var feed = new Feed(FeedType.Product);
            var request = new Request(feed, Request.GenerateId());
            var findResponce = Connector.FindRequest(request, RequestProcessingStage.Inprocess);
            var findResult = (FindRemoteFileResult)findResponce.Result;

            Trace.TraceInformation(findResponce.StatusDescription);

            Assert.AreEqual(StatusCode.FindRequestFail, findResponce.StatusCode);
            Assert.IsNull(findResult);
            Assert.IsTrue(findResponce.StatusDescription.Contains(StatusCode.FindRemoteFileFail.ToString()));
        }

        // --------------------------------------------------------[]
        [TestMethod]
        public void FindRequest_Inprocess_Wrong()
        {
            var feed = new Feed(FeedType.None);
            var request = new Request(feed, Request.GenerateId());
            var findResponce = Connector.FindRequest(request, RequestProcessingStage.Inprocess);
            var findResult = (FindRemoteFileResult)findResponce.Result;

            Trace.TraceInformation(findResponce.StatusDescription);

            Assert.AreEqual(StatusCode.FindRequestFail, findResponce.StatusCode);
            Assert.IsNull(findResult);
            Assert.IsTrue(findResponce.StatusDescription.Contains("Exception"));
        }

        // --------------------------------------------------------[]
        [TestMethod]
        public void FindRequest_Output()
        {
            var feed = new Feed(FeedType.Product);
            var sendResponse = Connector.SendTestFeed(feed);
            var request = new Request(feed, (Request.Identifier) sendResponse.Result);
            var findResponce = Connector.FindRequest(request, RequestProcessingStage.Output);
            var findResult = (FindRemoteFileResult)findResponce.Result;

            Trace.TraceInformation(findResult.FileName);
            Trace.TraceInformation(findResult.FolderPath);
            Trace.TraceInformation(findResponce.StatusDescription);

            Assert.AreEqual(StatusCode.FindRequestSuccess, findResponce.StatusCode);
            Assert.IsNotNull(findResult.FileName);
            Assert.IsNotNull(findResult.FolderPath);
            Assert.IsTrue(findResult.FileName.Length > 1);
            Assert.IsTrue(findResult.FolderPath.Length > 1);
        }

        // --------------------------------------------------------[]
        [TestMethod]
        public void FindRequest_Output_Unknown()
        {
            var feed = new Feed(FeedType.Product);
            var request = new Request(feed, Request.GenerateId());
            var findResponce = Connector.FindRequest(request, RequestProcessingStage.Output);
            var remoteFileName = (string) findResponce.Result;

            Trace.TraceInformation(findResponce.StatusDescription);

            Assert.AreEqual(StatusCode.FindRequestFail, findResponce.StatusCode);
            Assert.IsNull(remoteFileName);
        }

        // ===================================================================================== [] 
        // GetRequestStatus
        [TestMethod]
        public void GetRequestStatus_Success()
        {
            Assert.Inconclusive();
        }

        // --------------------------------------------------------[]
        [TestMethod]
        public void GetRequestStatus_Unknown()
        {
            Assert.Inconclusive();
        }

        // --------------------------------------------------------[]
        [TestMethod]
        public void GetRequestStatus_Inproc()
        {
            // Now: GetRequestStatus_Inproc
            var feed = new Feed(FeedType.Product);
            var sendResponse = Connector.SendTestFeed(feed);
            var request = new Request(feed, (Request.Identifier)sendResponse.Result);
            var requestResponce = Connector.GetRequestStatus(request);
            var requestResult = (GetRequetStatusResult)requestResponce.Result;

            Trace.TraceInformation(requestResponce.StatusDescription);

            Assert.AreEqual(StatusCode.GetRequestStatusSuccess, requestResponce.StatusCode);
            Assert.AreEqual(GetRequetStatusResult.Success, requestResult);
        }

        // --------------------------------------------------------[]
        [TestMethod]
        public void GetRequestStatus_Fail()
        {
            Assert.Inconclusive();
        }
    }
}