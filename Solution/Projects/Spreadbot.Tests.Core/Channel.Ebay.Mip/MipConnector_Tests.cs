﻿// Spreadbot (c) 2015 Crocodev
// Spreadbot.Tests.Core
// MipConnector_Tests.cs
// romak_000, 2015-03-19 15:37

using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spreadbot.Core.Channel.Ebay.Mip.Connector;
using Spreadbot.Core.Channel.Ebay.Mip.Feed;
using Spreadbot.Core.Channel.Ebay.Mip.Operations.Request;
using Spreadbot.Core.Channel.Ebay.Mip.Operations.StatusCode;

namespace Spreadbot.Tests.Core.Channel.Ebay.Mip
{
    [TestClass]
    public class MipConnector_Tests
    {
        // ===================================================================================== []
        // SendFeed
        [TestMethod]
        public void SendZippedFeedFolder()
        {
            var feed = new MipFeed(MipFeedType.Product);

            var response = MipConnector.SendZippedFeedFolder(feed);
            Trace.TraceInformation(response.Autoinfo);

            Assert.AreEqual(MipStatusCode.SendZippedFeedFolderSuccess, response.Code);
            Assert.IsTrue(MipRequest.VerifyRequestId(response.Result.MipRequestId));
        }

        // ===================================================================================== []
        // FindRequest
        [TestMethod]
        public void FindRequest_Inprocess()
        {
            var feed = new MipFeed(MipFeedType.Product);
            var sendResponse = MipConnector.SendZippedFeedFolder(feed);
            var request = new MipRequest(feed, sendResponse.Result.MipRequestId);

            var findResponse = MipConnector.FindRequest(request, MipRequestProcessingStage.Inprocess);
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
            var feed = new MipFeed(MipFeedType.Product);
            var request = new MipRequest(feed, MipRequest.GenerateId());

            var findResponse = MipConnector.FindRequest(request, MipRequestProcessingStage.Inprocess);
            Trace.TraceInformation(findResponse.Autoinfo);

            Assert.AreEqual(MipStatusCode.FindRequestFail, findResponse.Code);
            Assert.IsNull(findResponse.Result);
            Assert.IsTrue(findResponse.Autoinfo.Contains(MipStatusCode.FindRemoteFileFail.ToString()));
        }

        // --------------------------------------------------------[]
        [TestMethod]
        public void FindRequest_Inprocess_Wrong()
        {
            var feed = new MipFeed(MipFeedType.None);
            var request = new MipRequest(feed, MipRequest.GenerateId());

            var findResponse = MipConnector.FindRequest(request, MipRequestProcessingStage.Inprocess);
            Trace.TraceInformation(findResponse.Autoinfo);

            Assert.AreEqual(MipStatusCode.FindRequestFail, findResponse.Code);
            Assert.IsNull(findResponse.Result);
            Assert.IsTrue(findResponse.Autoinfo.Contains("Exception"));
        }

        // --------------------------------------------------------[]
        [TestMethod]
        public void FindRequest_Output()
        {
            var feed = new MipFeed(MipFeedType.Product);
            var sendResponse = MipConnector.SendTestFeedFolder(feed);
            var request = new MipRequest(feed, sendResponse.Result.MipRequestId);

            var findResponse = MipConnector.FindRequest(request, MipRequestProcessingStage.Output);
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
            var feed = new MipFeed(MipFeedType.Product);
            var request = new MipRequest(feed, MipRequest.GenerateId());

            var findResponse = MipConnector.FindRequest(request, MipRequestProcessingStage.Output);
            Trace.TraceInformation(findResponse.Autoinfo);

            Assert.AreEqual(MipStatusCode.FindRequestFail, findResponse.Code);
            Assert.IsNull(findResponse.Result);
        }

        // ===================================================================================== [] 
        // GetRequestStatus
        [TestMethod]
        public void GetRequestStatus_Inproc()
        {
            var feed = new MipFeed(MipFeedType.Availability);
            var sendResponse = MipConnector.SendZippedFeedFolder(feed);
            var request = new MipRequest(feed, sendResponse.Result.MipRequestId);

            var requestResponse = MipConnector.GetRequestStatus(request);
            Trace.TraceInformation(requestResponse.Autoinfo);

            Assert.AreEqual(MipStatusCode.GetRequestStatusSuccess, requestResponse.Code);
            Assert.AreEqual(MipRequestStatus.Inprocess, requestResponse.Result.MipRequestStatusCode);
        }

        // --------------------------------------------------------[]
        [TestMethod]
        public void GetRequestStatus_Success()
        {
            var feed = new MipFeed(MipFeedType.Distribution);
            var sendResponse = MipConnector.SendTestFeedFolder(feed);
            var request = new MipRequest(feed, sendResponse.Result.MipRequestId);

            var requestResponse = MipConnector.GetRequestStatus(request, ignoreInprocess: true);
            Trace.TraceInformation(requestResponse.Autoinfo);

            Assert.AreEqual(MipStatusCode.GetRequestStatusSuccess, requestResponse.Code);
            Assert.AreEqual(MipRequestStatus.Success, requestResponse.Result.MipRequestStatusCode);
        }

        // --------------------------------------------------------[]
        [TestMethod]
        public void GetRequestStatus_Unknown()
        {
            var feed = new MipFeed(MipFeedType.Product);
            var request = new MipRequest(feed, MipRequest.GenerateId());

            var requestResponse = MipConnector.GetRequestStatus(request);
            Trace.TraceInformation(requestResponse.Autoinfo);

            Assert.AreEqual(MipStatusCode.GetRequestStatusSuccess, requestResponse.Code);
            Assert.AreEqual(MipRequestStatus.Unknown, requestResponse.Result.MipRequestStatusCode);
        }
    }
}