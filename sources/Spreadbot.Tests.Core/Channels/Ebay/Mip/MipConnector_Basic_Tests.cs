// Spreadbot (c) 2015 Crocodev
// Spreadbot.Tests.Core
// MipConnector_Basic_Tests.cs
// Roman, 2015-04-01 4:59 PM

using System;
using NUnit.Framework;
using Spreadbot.Core.Channels.Ebay.Mip.Connector;
using Spreadbot.Core.Channels.Ebay.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Request;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.StatusCode;
using Spreadbot.Sdk.Common.Exceptions;
using Spreadbot.Tests.Core.Common;

namespace Spreadbot.Tests.Core.Channels.Ebay.Mip
{
    [TestFixture]
    public class MipConnector_Basic_Tests : SpreadbotBaseTest
    {
        // ===================================================================================== []
        [SetUp]
        public static void Init()
        {
            MipConnectorTestInitializer.PrepareTestFiles();
        }

        // ===================================================================================== []
        // SendFeed
        [Test]
        public void SendZippedFeedFolder()
        {
            var feed = new MipFeedHandler( MipFeedType.Product );

            var response = MipConnector.SendZippedFeedFolder( feed );
            Console.WriteLine( response );
            IgnoreMipQueueDepthErrorMessage( response.ToString() );

            Assert.AreEqual( MipOperationStatus.SendZippedFeedFolderSuccess, response.Code );
            Assert.IsTrue( MipRequestHandler.VerifyRequestId( response.Result.MipRequestId ) );
        }

        // ===================================================================================== []
        // FindRequest
        [Test]
        public void FindRequest_Inprocess()
        {
            var feed = new MipFeedHandler( MipFeedType.Product );
            var sendResponse = MipConnector.SendZippedFeedFolder( feed );
            Console.WriteLine( sendResponse );
            Assert.IsNotNull( sendResponse.Result );

            var request = new MipRequestHandler( feed, sendResponse.Result.MipRequestId );
            var findResponse = MipConnector.FindRequest( request, MipRequestProcessingStage.Inprocess );
            Console.WriteLine( findResponse );

            Assert.AreEqual( MipOperationStatus.FindRequestSuccess, findResponse.Code );
            Assert.IsNotNull( findResponse.Result.RemoteFileName );
            Assert.IsNotNull( findResponse.Result.RemoteFolderPath );
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

            Assert.That( findResponse.Code.Equals( MipOperationStatus.FindRequestFailure ) );
            Assert.That( findResponse.Result == null );
            Assert.That( findResponse.ToString().Contains( MipOperationStatus.FindRemoteFileFailure.ToString() ),
                "FindResponse Contains 'FindRemoteFileFailure'" );
        }

        // --------------------------------------------------------[]
        [Test]
        public void FindRequest_Inprocess_Wrong()
        {
            var feed = new MipFeedHandler( MipFeedType.None );
            var request = new MipRequestHandler( feed, MipRequestHandler.GenerateId() );

            var findResponse = MipConnector.FindRequest( request, MipRequestProcessingStage.Inprocess );
            Console.WriteLine( findResponse );

            Assert.AreEqual( MipOperationStatus.FindRequestFailure, findResponse.Code );
            Assert.IsNull( findResponse.Result );
            Assert.IsTrue( findResponse.ToString().Contains( "Exception" ) );
        }

        // --------------------------------------------------------[]
        [Test]
        public void FindRequest_Output()
        {
            try {
                var feed = new MipFeedHandler( MipFeedType.Product );
                var sendResponse = MipConnector.SendTestFeedFolder( feed );
                Console.WriteLine( sendResponse );
                Assert.IsNotNull( sendResponse.Result );

                var request = new MipRequestHandler( feed, sendResponse.Result.MipRequestId );
                var findResponse = MipConnector.FindRequest( request, MipRequestProcessingStage.Output );
                Console.WriteLine( findResponse );

                Assert.AreEqual( MipOperationStatus.FindRequestSuccess, findResponse.Code );
                Assert.IsNotNull( findResponse.Result.RemoteFileName );
                Assert.IsNotNull( findResponse.Result.RemoteFolderPath );
                Assert.IsTrue( findResponse.Result.RemoteFileName.Length > 1 );
                Assert.IsTrue( findResponse.Result.RemoteFolderPath.Length > 1 );
            }
            catch( SpreadbotException exception ) {
                IgnoreMipQueueDepthErrorMessage( exception.Message );
            }
        }

        // --------------------------------------------------------[]
        [Test]
        public void FindRequest_Output_Unknown()
        {
            var feed = new MipFeedHandler( MipFeedType.Product );
            var request = new MipRequestHandler( feed, MipRequestHandler.GenerateId() );

            var findResponse = MipConnector.FindRequest( request, MipRequestProcessingStage.Output );
            Console.WriteLine( findResponse );

            Assert.AreEqual( MipOperationStatus.FindRequestFailure, findResponse.Code );
            Assert.IsNull( findResponse.Result );
        }

        // ===================================================================================== [] 
        // GetRequestStatus
        [Test]
        public void GetRequestStatus_Inproc()
        {
            var feed = new MipFeedHandler( MipFeedType.Availability );
            var sendResponse = MipConnector.SendZippedFeedFolder( feed );
            var request = new MipRequestHandler( feed, sendResponse.Result.MipRequestId );

            var requestResponse = MipConnector.GetRequestStatus( request );
            Console.WriteLine( requestResponse );

            Assert.AreEqual( MipOperationStatus.GetRequestStatusSuccess, requestResponse.Code );
            Assert.AreEqual( MipRequestStatus.Inprocess, requestResponse.Result.MipRequestStatusCode );
        }

        // --------------------------------------------------------[]
        [Test]
        public void GetRequestStatus_Success()
        {
            var feed = new MipFeedHandler( MipFeedType.Distribution );
            var sendResponse = MipConnector.SendTestFeedFolder( feed );
            IgnoreMipQueueDepthErrorMessage( sendResponse.ToString() );

            Console.WriteLine( sendResponse );
            Assert.IsNotNull( sendResponse.Result );

            var request = new MipRequestHandler( feed, sendResponse.Result.MipRequestId );
            var requestResponse = MipConnector.GetRequestStatus( request, ignoreInprocess : true );
            Console.WriteLine( requestResponse );

            Assert.AreEqual( MipOperationStatus.GetRequestStatusSuccess, requestResponse.Code );
            Assert.AreEqual( MipRequestStatus.Success, requestResponse.Result.MipRequestStatusCode );
        }

        // --------------------------------------------------------[]
        [Test]
        public void GetRequestStatus_Unknown()
        {
            var feed = new MipFeedHandler( MipFeedType.Product );
            var request = new MipRequestHandler( feed, MipRequestHandler.GenerateId() );

            var requestResponse = MipConnector.GetRequestStatus( request );
            Console.WriteLine( requestResponse );

            Assert.AreEqual( MipOperationStatus.GetRequestStatusSuccess, requestResponse.Code );
            Assert.AreEqual( MipRequestStatus.Unknown, requestResponse.Result.MipRequestStatusCode );
        }
    }
}