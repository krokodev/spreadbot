﻿// Spreadbot (c) 2015 Crocodev
// Spreadbot.Tests.Core
// MipConnector_Basic_Tests.cs
// romak_000, 2015-03-24 11:27

using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spreadbot.Core.Channels.Ebay.Mip.Connector;
using Spreadbot.Core.Channels.Ebay.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Request;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.StatusCode;

namespace Spreadbot.Tests.Core.Channels.Ebay.Mip
{
    [TestClass]
    public class MipConnector_Basic_Tests
    {
        // ===================================================================================== []
        [ClassInitialize]
        public static void Init( TestContext testContext )
        {
            MipConnectorTestInitializer.PrepareTestFiles();
        }

        // ===================================================================================== []
        // SendFeed
        [TestMethod]
        public void SendZippedFeedFolder()
        {
            var feed = new MipFeedHandler( MipFeedType.Product );

            var response = MipConnector.SendZippedFeedFolder( feed );
            Trace.TraceInformation( response.Autoinfo );

            Assert.AreEqual( MipOperationStatus.SendZippedFeedFolderSuccess, response.Code );
            Assert.IsTrue( MipRequestHandler.VerifyRequestId( response.Result.MipRequestId ) );
        }

        // ===================================================================================== []
        // FindRequest
        [TestMethod]
        public void FindRequest_Inprocess()
        {
            var feed = new MipFeedHandler( MipFeedType.Product );
            var sendResponse = MipConnector.SendZippedFeedFolder( feed );
            var request = new MipRequestHandler( feed, sendResponse.Result.MipRequestId );

            var findResponse = MipConnector.FindRequest( request, MipRequestProcessingStage.Inprocess );
            Trace.TraceInformation( findResponse.Autoinfo );

            Assert.AreEqual( MipOperationStatus.FindRequestSuccess, findResponse.Code );
            Assert.IsNotNull( findResponse.Result.RemoteFileName );
            Assert.IsNotNull( findResponse.Result.RemoteFolderPath );
            Assert.IsTrue( findResponse.Result.RemoteFileName.Length > 1 );
        }

        // --------------------------------------------------------[]
        [TestMethod]
        public void FindRequest_Inprocess_Unknown()
        {
            var feed = new MipFeedHandler( MipFeedType.Product );
            var request = new MipRequestHandler( feed, MipRequestHandler.GenerateId() );

            var findResponse = MipConnector.FindRequest( request, MipRequestProcessingStage.Inprocess );
            Trace.TraceInformation( findResponse.Autoinfo );

            Assert.AreEqual( MipOperationStatus.FindRequestFailure, findResponse.Code );
            Assert.IsNull( findResponse.Result );
            Assert.IsTrue( findResponse.Autoinfo.Contains( MipOperationStatus.FindRemoteFileFailure.ToString() ) );
        }

        // --------------------------------------------------------[]
        [TestMethod]
        public void FindRequest_Inprocess_Wrong()
        {
            var feed = new MipFeedHandler( MipFeedType.None );
            var request = new MipRequestHandler( feed, MipRequestHandler.GenerateId() );

            var findResponse = MipConnector.FindRequest( request, MipRequestProcessingStage.Inprocess );
            Trace.TraceInformation( findResponse.Autoinfo );

            Assert.AreEqual( MipOperationStatus.FindRequestFailure, findResponse.Code );
            Assert.IsNull( findResponse.Result );
            Assert.IsTrue( findResponse.Autoinfo.Contains( "Exception" ) );
        }

        // --------------------------------------------------------[]
        [TestMethod]
        public void FindRequest_Output()
        {
            var feed = new MipFeedHandler( MipFeedType.Product );
            var sendResponse = MipConnector.SendTestFeedFolder( feed );
            var request = new MipRequestHandler( feed, sendResponse.Result.MipRequestId );

            var findResponse = MipConnector.FindRequest( request, MipRequestProcessingStage.Output );
            Trace.TraceInformation( findResponse.Autoinfo );

            Assert.AreEqual( MipOperationStatus.FindRequestSuccess, findResponse.Code );
            Assert.IsNotNull( findResponse.Result.RemoteFileName );
            Assert.IsNotNull( findResponse.Result.RemoteFolderPath );
            Assert.IsTrue( findResponse.Result.RemoteFileName.Length > 1 );
            Assert.IsTrue( findResponse.Result.RemoteFolderPath.Length > 1 );
        }

        // --------------------------------------------------------[]
        [TestMethod]
        public void FindRequest_Output_Unknown()
        {
            var feed = new MipFeedHandler( MipFeedType.Product );
            var request = new MipRequestHandler( feed, MipRequestHandler.GenerateId() );

            var findResponse = MipConnector.FindRequest( request, MipRequestProcessingStage.Output );
            Trace.TraceInformation( findResponse.Autoinfo );

            Assert.AreEqual( MipOperationStatus.FindRequestFailure, findResponse.Code );
            Assert.IsNull( findResponse.Result );
        }

        // ===================================================================================== [] 
        // GetRequestStatus
        [TestMethod]
        public void GetRequestStatus_Inproc()
        {
            var feed = new MipFeedHandler( MipFeedType.Availability );
            var sendResponse = MipConnector.SendZippedFeedFolder( feed );
            var request = new MipRequestHandler( feed, sendResponse.Result.MipRequestId );

            var requestResponse = MipConnector.GetRequestStatus( request );
            Trace.TraceInformation( requestResponse.Autoinfo );

            Assert.AreEqual( MipOperationStatus.GetRequestStatusSuccess, requestResponse.Code );
            Assert.AreEqual( MipRequestStatus.Inprocess, requestResponse.Result.MipRequestStatusCode );
        }

        // --------------------------------------------------------[]
        [TestMethod]
        public void GetRequestStatus_Success()
        {
            var feed = new MipFeedHandler( MipFeedType.Distribution );
            var sendResponse = MipConnector.SendTestFeedFolder( feed );
            var request = new MipRequestHandler( feed, sendResponse.Result.MipRequestId );

            var requestResponse = MipConnector.GetRequestStatus( request, ignoreInprocess : true );
            Trace.TraceInformation( requestResponse.Autoinfo );

            Assert.AreEqual( MipOperationStatus.GetRequestStatusSuccess, requestResponse.Code );
            Assert.AreEqual( MipRequestStatus.Success, requestResponse.Result.MipRequestStatusCode );
        }

        // --------------------------------------------------------[]
        [TestMethod]
        public void GetRequestStatus_Unknown()
        {
            var feed = new MipFeedHandler( MipFeedType.Product );
            var request = new MipRequestHandler( feed, MipRequestHandler.GenerateId() );

            var requestResponse = MipConnector.GetRequestStatus( request );
            Trace.TraceInformation( requestResponse.Autoinfo );

            Assert.AreEqual( MipOperationStatus.GetRequestStatusSuccess, requestResponse.Code );
            Assert.AreEqual( MipRequestStatus.Unknown, requestResponse.Result.MipRequestStatusCode );
        }
    }
}