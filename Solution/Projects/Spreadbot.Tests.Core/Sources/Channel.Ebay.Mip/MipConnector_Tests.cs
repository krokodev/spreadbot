using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Spreadbot.Core.Channel.Ebay.Mip.Tests
{
    [TestClass]
    public class MipConnector_Tests
    {
        // ===================================================================================== []
        // SendFeed
        [TestMethod]
        public void SendFeed()
        {
            var feed = new Feed(FeedType.Product);

            var response = MipConnector.SendFeedFolder(feed);
            Trace.TraceInformation(response.Autoinfo);

            Assert.AreEqual(MipStatusCode.SendFeedFolderSuccess, response.Code);
            Assert.IsTrue(Request.VerifyRequestId(response.Result.RequestId));
        }

        // ===================================================================================== []
        // FindRequest
        [TestMethod]
        public void FindRequest_Inprocess()
        {
            var feed = new Feed(FeedType.Product);
            var sendResponse = MipConnector.SendFeedFolder(feed);
            var request = new Request(feed, sendResponse.Result.RequestId);

            var findResponse = MipConnector.FindRequest(request, RequestProcessingStage.Inprocess);
            Trace.TraceInformation(findResponse.Autoinfo);

            Assert.AreEqual(MipStatusCode.FindRequestSuccess, findResponse.Code);
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

            var findResponse = MipConnector.FindRequest(request, RequestProcessingStage.Inprocess);
            Trace.TraceInformation(findResponse.Autoinfo);

            Assert.AreEqual(MipStatusCode.FindRequestFail, findResponse.Code);
            Assert.IsNull(findResponse.Result);
            Assert.IsTrue(findResponse.Autoinfo.Contains(MipStatusCode.FindRemoteFileFail.ToString()));
        }

        // --------------------------------------------------------[]
        [TestMethod]
        public void FindRequest_Inprocess_Wrong()
        {
            var feed = new Feed(FeedType.None);
            var request = new Request(feed, Request.GenerateId());

            var findResponse = MipConnector.FindRequest(request, RequestProcessingStage.Inprocess);
            Trace.TraceInformation(findResponse.Autoinfo);

            Assert.AreEqual(MipStatusCode.FindRequestFail, findResponse.Code);
            Assert.IsNull(findResponse.Result);
            Assert.IsTrue(findResponse.Autoinfo.Contains("Exception"));
        }

        // --------------------------------------------------------[]
        [TestMethod]
        public void FindRequest_Output()
        {
            var feed = new Feed(FeedType.Product);
            var sendResponse = MipConnector.SendTestFeedFolder(feed);
            var request = new Request(feed, sendResponse.Result.RequestId);

            var findResponse = MipConnector.FindRequest(request, RequestProcessingStage.Output);
            Trace.TraceInformation(findResponse.Autoinfo);

            Assert.AreEqual(MipStatusCode.FindRequestSuccess, findResponse.Code);
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

            var findResponse = MipConnector.FindRequest(request, RequestProcessingStage.Output);
            Trace.TraceInformation(findResponse.Autoinfo);

            Assert.AreEqual(MipStatusCode.FindRequestFail, findResponse.Code);
            Assert.IsNull(findResponse.Result);
        }

        // ===================================================================================== [] 
        // GetRequestStatus
        [TestMethod]
        public void GetRequestStatus_Inproc()
        {
            var feed = new Feed(FeedType.Availability);
            var sendResponse = MipConnector.SendFeedFolder(feed);
            var request = new Request(feed, sendResponse.Result.RequestId);

            var requestResponse = MipConnector.GetRequestStatus(request);
            Trace.TraceInformation(requestResponse.Autoinfo);

            Assert.AreEqual(MipStatusCode.GetRequestStatusSuccess, requestResponse.Code);
            Assert.AreEqual(RequetStatus.Inprocess, requestResponse.Result.Status);
        }
        // --------------------------------------------------------[]
        [TestMethod]
        public void GetRequestStatus_Success()
        {
            var feed = new Feed(FeedType.Distribution);
            var sendResponse = MipConnector.SendTestFeedFolder(feed);
            var request = new Request(feed, sendResponse.Result.RequestId);

            var requestResponse = MipConnector.GetRequestStatus(request, ignoreInprocess: true);
            Trace.TraceInformation(requestResponse.Autoinfo);

            Assert.AreEqual(MipStatusCode.GetRequestStatusSuccess, requestResponse.Code);
            Assert.AreEqual(RequetStatus.Success, requestResponse.Result.Status);
        }

        // --------------------------------------------------------[]
        [TestMethod]
        public void GetRequestStatus_Unknown()
        {
            var feed = new Feed(FeedType.Product);
            var request = new Request(feed, Request.GenerateId());

            var requestResponse = MipConnector.GetRequestStatus(request);
            Trace.TraceInformation(requestResponse.Autoinfo);

            Assert.AreEqual(MipStatusCode.GetRequestStatusSuccess, requestResponse.Code);
            Assert.AreEqual(RequetStatus.Unknown, requestResponse.Result.Status);
        }
    }
}