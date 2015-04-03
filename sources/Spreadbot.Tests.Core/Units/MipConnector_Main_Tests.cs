﻿// Spreadbot (c) 2015 Crocodev
// Spreadbot.Tests.Core
// MipConnector_Main_Tests.cs
// Roman, 2015-04-03 1:45 PM

using System;
using NUnit.Framework;
using Spreadbot.Core.Channels.Ebay.Mip.Connector;
using Spreadbot.Core.Channels.Ebay.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Request;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.StatusCode;
using Spreadbot.Tests.Core.Code;

namespace Spreadbot.Tests.Core.Units
{
    [TestFixture]
    public class MipConnector_Main_Tests : SpreadbotTestBase
    {
        // --------------------------------------------------------[]
        [SetUp]
        public static void Init()
        {
            MipConnectorTestInitializer.PrepareTestFiles();
        }

        // --------------------------------------------------------[]
        [Test]
        public void SendZippedFeedFolder()
        {
            var feed = new MipFeedHandler( MipFeedType.Product );

            var response = MipConnector.SendFeed( feed );
            Console.WriteLine( response );
            IgnoreMipQueueDepthErrorMessage( response );

            Assert.AreEqual( MipOperationStatus.SendFeedSuccess, response.StatusCode );
            Assert.IsTrue( MipRequestHandler.VerifyRequestId( response.Result.MipRequestId ) );
            Assert_That_Text_Contains( response, "InnerResponses" );
            Assert_That_Text_Contains( response, MipOperationStatus.ZipFeedSuccess );
            Assert_That_Text_Contains( response, MipOperationStatus.SftpSendFilesSuccess );
        }


        // --------------------------------------------------------[]
        [Test]
        public void FindRequest_Inprocess()
        {
            var feed = new MipFeedHandler( MipFeedType.Product );
            var sendResponse = MipConnector.SendFeed( feed );
            IgnoreMipQueueDepthErrorMessage( sendResponse );

            Console.WriteLine( sendResponse );

            Assert.IsNotNull( sendResponse.Result, "Result" );
            Assert.IsNotNull( sendResponse.InnerResponses, "InnerResponses" );

            Assert_That_Text_Contains( sendResponse, MipOperationStatus.ZipFeedSuccess );
            Assert_That_Text_Contains( sendResponse, MipOperationStatus.SftpSendFilesSuccess );

            var request = new MipRequestHandler( feed, sendResponse.Result.MipRequestId );
            var findResponse = MipConnector.FindRequest( request, MipRequestProcessingStage.Inprocess );
            Console.WriteLine();
            Console.WriteLine( findResponse );

            Assert.AreEqual( MipOperationStatus.FindRequestSuccess, findResponse.StatusCode );
            Assert.IsNotNull( findResponse.Result.RemoteFileName );
            Assert.IsNotNull( findResponse.Result.RemoteDir );
            Assert.IsTrue( findResponse.Result.RemoteFileName.Length > 1 );
        }

        // --------------------------------------------------------[]
        [Test]
        public void FindRequest_Inprocess_Unknown()
        {
            var feed = new MipFeedHandler( MipFeedType.Product );
            var request = new MipRequestHandler( feed, MipRequestHandler.GenerateId() );

            var findResponse = MipConnector.FindRequest( request, MipRequestProcessingStage.Inprocess );
            Console.WriteLine( findResponse );

            Assert.That( findResponse.StatusCode.Equals( MipOperationStatus.FindRequestFailure ) );
            Assert.That( findResponse.Result == null );
            Assert_That_Text_Contains( findResponse, MipOperationStatus.FindRequestFailure );
            Assert_That_Text_Contains( findResponse, @"not found in [store/product/inprocess]" );
        }

        // --------------------------------------------------------[]
        [Test]
        public void FindRequest_Inprocess_Wrong()
        {
            var feed = new MipFeedHandler( MipFeedType.None );
            var request = new MipRequestHandler( feed, MipRequestHandler.GenerateId() );

            var findResponse = MipConnector.FindRequest( request, MipRequestProcessingStage.Inprocess );
            Console.WriteLine( findResponse );

            Assert.AreEqual( MipOperationStatus.FindRequestFailure, findResponse.StatusCode );
            Assert.IsNull( findResponse.Result );
            Assert.IsTrue( findResponse.ToString().Contains( "Exception" ) );
        }

        // --------------------------------------------------------[]
        [Test]
        public void FindRequest_Output()
        {
            var feed = new MipFeedHandler( MipFeedType.Product );
            var sendResponse = MipConnector.SendTestFeed( feed );
            IgnoreMipQueueDepthErrorMessage( sendResponse );

            Console.WriteLine( sendResponse );
            Assert.IsNotNull( sendResponse.Result );

            var request = new MipRequestHandler( feed, sendResponse.Result.MipRequestId );
            var findResponse = MipConnector.FindRequest( request, MipRequestProcessingStage.Output );
            Console.WriteLine( findResponse );

            Assert.AreEqual( MipOperationStatus.FindRequestSuccess, findResponse.StatusCode );
            Assert.IsNotNull( findResponse.Result.RemoteFileName );
            Assert.IsNotNull( findResponse.Result.RemoteDir );
            Assert.IsTrue( findResponse.Result.RemoteFileName.Length > 1 );
            Assert.IsTrue( findResponse.Result.RemoteDir.Length > 1 );
        }

        // --------------------------------------------------------[]
        [Test]
        public void FindRequest_Output_Unknown()
        {
            var feed = new MipFeedHandler( MipFeedType.Product );
            var request = new MipRequestHandler( feed, MipRequestHandler.GenerateId() );

            var findResponse = MipConnector.FindRequest( request, MipRequestProcessingStage.Output );
            
            Console.WriteLine( findResponse );

            Assert.AreEqual( MipOperationStatus.FindRequestFailure, findResponse.StatusCode );
            Assert.IsNull( findResponse.Result );
        }

        // ===================================================================================== [] 
        // GetRequestStatus
        [Test]
        public void GetRequestStatus_Inproc()
        {
            var feed = new MipFeedHandler( MipFeedType.Product );
            var sendResponse = MipConnector.SendFeed( feed );
            IgnoreMipQueueDepthErrorMessage( sendResponse );

            var request = new MipRequestHandler( feed, sendResponse.Result.MipRequestId );
            var requestResponse = MipConnector.GetRequestStatus( request );
            Console.WriteLine( requestResponse );

            Assert.AreEqual( MipOperationStatus.GetRequestStatusSuccess, requestResponse.StatusCode );
            Assert.AreEqual( MipRequestStatus.Inprocess, requestResponse.Result.MipRequestStatusCode );
        }

        // --------------------------------------------------------[]
        [Ignore("Waiting for Fakes")]
        [Test]
        public void GetRequestStatus_Success()
        {
/*
            var feed = new MipFeedHandler( MipFeedType.Availability );
            var sendResponse = MipConnector.SendTestFeed( feed );
            IgnoreMipQueueDepthErrorMessage( sendResponse );

            Console.WriteLine( sendResponse );
            Assert.IsNotNull( sendResponse.Result );

            var request = new MipRequestHandler( feed, sendResponse.Result.MipRequestId );
            var requestResponse = MipConnector.GetRequestStatus( request, ignoreInprocess : true );
            Console.WriteLine( requestResponse );

            Assert.AreEqual( MipOperationStatus.GetRequestStatusSuccess, requestResponse.StatusCode );
            Assert.AreEqual( MipRequestStatus.Success, requestResponse.Result.MipRequestStatusCode );
*/
            Assert.Fail();
        }

        // --------------------------------------------------------[]
        [Test]
        public void GetRequestStatus_Unknown()
        {
            var feed = new MipFeedHandler( MipFeedType.Product );
            var request = new MipRequestHandler( feed, MipRequestHandler.GenerateId() );

            var requestResponse = MipConnector.GetRequestStatus( request );
            Console.WriteLine( requestResponse );

            Assert.AreEqual( MipOperationStatus.GetRequestStatusSuccess, requestResponse.StatusCode );
            Assert.AreEqual( MipRequestStatus.Unknown, requestResponse.Result.MipRequestStatusCode );
        }

        // --------------------------------------------------------[]
        [Test]
        public void Response_Contains_ArgsInfo()
        {
            var feed = new MipFeedHandler( MipFeedType.None );
            var request = new MipRequestHandler( feed, MipRequestHandler.GenerateId() );

            var requestResponse = MipConnector.GetRequestStatus( request );
            Console.WriteLine( requestResponse );

            Assert_That_Text_Contains( requestResponse, "ArgsInfo" );
        }
    }
}