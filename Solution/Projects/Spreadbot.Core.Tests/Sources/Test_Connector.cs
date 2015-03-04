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
            Trace.TraceInformation(response.Description);

            Assert.AreEqual(StatusCode.SendFeedSuccess, response.Code);
            Assert.IsTrue(Request.VerifyRequestId(response.Result.RequestId));
        }

        // ===================================================================================== []
        // FindRequest
        [TestMethod]
        public void FindRequest_Inprocess()
        {
            var feed = new Feed(FeedType.Product);
            var sendResponse = Connector.SendFeed(feed);
            var request = new Request(feed, sendResponse.Result.RequestId);

            var findResponse = Connector.FindRequest(request, RequestProcessingStage.Inprocess);
            Trace.TraceInformation(findResponse.Description);

            Assert.AreEqual(StatusCode.FindRequestSuccess, findResponse.Code);
            Assert.IsNotNull(findResponse.Result.FileName);
            Assert.IsNotNull(findResponse.Result.FolderPath);
            Assert.IsTrue(findResponse.Result.FileName.Length > 1);
        }

        // --------------------------------------------------------[]
        [TestMethod]
        public void FindRequest_Inprocess_Unknown()
        {
            var feed = new Feed(FeedType.Product);
            var request = new Request(feed, Request.GenerateId());

            var findResponse = Connector.FindRequest(request, RequestProcessingStage.Inprocess);
            Trace.TraceInformation(findResponse.Description);

            Assert.AreEqual(StatusCode.FindRequestFail, findResponse.Code);
            Assert.IsNull(findResponse.Result);
            Assert.IsTrue(findResponse.Description.Contains(StatusCode.FindRemoteFileFail.ToString()));
        }

        // --------------------------------------------------------[]
        [TestMethod]
        public void FindRequest_Inprocess_Wrong()
        {
            var feed = new Feed(FeedType.None);
            var request = new Request(feed, Request.GenerateId());

            var findResponse = Connector.FindRequest(request, RequestProcessingStage.Inprocess);
            Trace.TraceInformation(findResponse.Description);

            Assert.AreEqual(StatusCode.FindRequestFail, findResponse.Code);
            Assert.IsNull(findResponse.Result);
            Assert.IsTrue(findResponse.Description.Contains("Exception"));
        }

        // --------------------------------------------------------[]
        [TestMethod]
        public void FindRequest_Output()
        {
            var feed = new Feed(FeedType.Product);
            var sendResponse = Connector.SendTestFeed(feed);
            var request = new Request(feed, sendResponse.Result.RequestId);

            var findResponse = Connector.FindRequest(request, RequestProcessingStage.Output);
            Trace.TraceInformation(findResponse.Description);

            Assert.AreEqual(StatusCode.FindRequestSuccess, findResponse.Code);
            Assert.IsNotNull(findResponse.Result.FileName);
            Assert.IsNotNull(findResponse.Result.FolderPath);
            Assert.IsTrue(findResponse.Result.FileName.Length > 1);
            Assert.IsTrue(findResponse.Result.FolderPath.Length > 1);
        }

        // --------------------------------------------------------[]
        [TestMethod]
        public void FindRequest_Output_Unknown()
        {
            var feed = new Feed(FeedType.Product);
            var request = new Request(feed, Request.GenerateId());

            var findResponse = Connector.FindRequest(request, RequestProcessingStage.Output);
            Trace.TraceInformation(findResponse.Description);

            Assert.AreEqual(StatusCode.FindRequestFail, findResponse.Code);
            Assert.IsNull(findResponse.Result);
        }

        // ===================================================================================== [] 
        // GetRequestStatus
        [TestMethod]
        public void GetRequestStatus_Inproc()
        {
            var feed = new Feed(FeedType.Product);
            var sendResponse = Connector.SendTestFeed(feed);
            var request = new Request(feed, sendResponse.Result.RequestId);

            var requestResponse = Connector.GetRequestStatus(request);
            Trace.TraceInformation(requestResponse.Description);

            Assert.AreEqual(StatusCode.GetRequestStatusSuccess, requestResponse.Code);
            Assert.AreEqual(RequetStatus.Inprocess, requestResponse.Result.Status);
        }
        // --------------------------------------------------------[]
        [TestMethod]
        public void GetRequestStatus_Success()
        {
            // Now: GetRequestStatus_Success

            Assert.Inconclusive();
        }

        // --------------------------------------------------------[]
        [TestMethod]
        public void GetRequestStatus_Unknown()
        {
            var feed = new Feed(FeedType.Product);
            var request = new Request(feed, Request.GenerateId());

            var requestResponse = Connector.GetRequestStatus(request);
            Trace.TraceInformation(requestResponse.Description);

            Assert.AreEqual(StatusCode.GetRequestStatusSuccess, requestResponse.Code);
            Assert.AreEqual(RequetStatus.Unknown, requestResponse.Result.Status);
        }


        // --------------------------------------------------------[]
        [TestMethod]
        public void GetRequestStatus_Fail()
        {
            Assert.Inconclusive();
        }
    }
}