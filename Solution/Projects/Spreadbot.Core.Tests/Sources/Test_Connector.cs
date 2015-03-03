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

            var findResponse = Connector.FindRequest(request, RequestProcessingStage.Inprocess);
            var findResult = (FindRemoteFileResult)findResponse.Result;

            Trace.TraceInformation(findResponse.StatusDescription);

            Assert.AreEqual(StatusCode.FindRequestSuccess, findResponse.StatusCode);
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

            var findResponse = Connector.FindRequest(request, RequestProcessingStage.Inprocess);
            var findResult = (FindRemoteFileResult)findResponse.Result;

            Trace.TraceInformation(findResponse.StatusDescription);

            Assert.AreEqual(StatusCode.FindRequestFail, findResponse.StatusCode);
            Assert.IsNull(findResult);
            Assert.IsTrue(findResponse.StatusDescription.Contains(StatusCode.FindRemoteFileFail.ToString()));
        }

        // --------------------------------------------------------[]
        [TestMethod]
        public void FindRequest_Inprocess_Wrong()
        {
            var feed = new Feed(FeedType.None);
            var request = new Request(feed, Request.GenerateId());

            var findResponse = Connector.FindRequest(request, RequestProcessingStage.Inprocess);
            var findResult = (FindRemoteFileResult)findResponse.Result;

            Trace.TraceInformation(findResponse.StatusDescription);

            Assert.AreEqual(StatusCode.FindRequestFail, findResponse.StatusCode);
            Assert.IsNull(findResult);
            Assert.IsTrue(findResponse.StatusDescription.Contains("Exception"));
        }

        // --------------------------------------------------------[]
        [TestMethod]
        public void FindRequest_Output()
        {
            var feed = new Feed(FeedType.Product);
            var sendResponse = Connector.SendTestFeed(feed);
            var request = new Request(feed, (Request.Identifier) sendResponse.Result);

            var findResponse = Connector.FindRequest(request, RequestProcessingStage.Output);
            var findResult = (FindRemoteFileResult)findResponse.Result;

            Trace.TraceInformation(findResult.FileName);
            Trace.TraceInformation(findResult.FolderPath);
            Trace.TraceInformation(findResponse.StatusDescription);

            Assert.AreEqual(StatusCode.FindRequestSuccess, findResponse.StatusCode);
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

            var findResponse = Connector.FindRequest(request, RequestProcessingStage.Output);
            var remoteFileName = (string) findResponse.Result;

            Trace.TraceInformation(findResponse.StatusDescription);

            Assert.AreEqual(StatusCode.FindRequestFail, findResponse.StatusCode);
            Assert.IsNull(remoteFileName);
        }

        // ===================================================================================== [] 
        // GetRequestStatus
        [TestMethod]
        public void GetRequestStatus_Inproc()
        {
            var feed = new Feed(FeedType.Product);
            var sendResponse = Connector.SendTestFeed(feed);
            var request = new Request(feed, (Request.Identifier)sendResponse.Result);

            var requestResponse = Connector.GetRequestStatus(request);
            var requestResult = (GetRequetStatusResult)requestResponse.Result;

            Trace.TraceInformation(requestResponse.StatusDescription);

            Assert.AreEqual(StatusCode.GetRequestStatusSuccess, requestResponse.StatusCode);
            Assert.AreEqual(GetRequetStatusResult.Inprocess, requestResult);
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
            var requestResult = (GetRequetStatusResult)requestResponse.Result;

            Trace.TraceInformation(requestResponse.StatusDescription);

            Assert.AreEqual(StatusCode.GetRequestStatusSuccess, requestResponse.StatusCode);
            Assert.AreEqual(GetRequetStatusResult.Unknown, requestResult);
        }


        // --------------------------------------------------------[]
        [TestMethod]
        public void GetRequestStatus_Fail()
        {
            Assert.Inconclusive();
        }
    }
}