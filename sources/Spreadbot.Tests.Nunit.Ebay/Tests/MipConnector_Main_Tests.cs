// Spreadbot (c) 2015 Krokodev
// Spreadbot.Tests.Nunit.Ebay
// MipConnector_Main_Tests.cs

using System;
using NUnit.Framework;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Connector;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Operations.FeedSubmission;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Operations.Statuses;
using Spreadbot.Nunit.Ebay.Base;
using Spreadbot.Nunit.Ebay.Mocks;
using Spreadbot.Nunit.Ebay.Utils;

namespace Spreadbot.Nunit.Ebay.Tests
{
    [TestFixture]
    public class MipConnectorMain_Tests : Ebay_Tests
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
            var feed = new MipFeedDescriptor( MipFeedType.Product );

            var response = MipConnector.Instance.SubmitFeed( feed );
            Console.WriteLine( response );
            IgnoreMipQueueDepthErrorMessage( response );

            Assert.AreEqual( MipOperationStatus.SubmitFeedSuccess, response.StatusCode );
            Assert.IsTrue( MipFeedSubmissionDescriptor.VerifySubmissionId( response.Result.FeedSubmissionId ) );
            Assert_That_Text_Contains( response, "InnerResponses" );
            Assert_That_Text_Contains( response, MipOperationStatus.ZipFeedSuccess );
            Assert_That_Text_Contains( response, MipOperationStatus.SftpSendFilesSuccess );
        }

        // --------------------------------------------------------[]
        [Test]
        public void FindRequest_Inprocess()
        {
            var feed = new MipFeedDescriptor( MipFeedType.Product );
            var sendResponse = MipConnector.Instance.SubmitFeed( feed );
            IgnoreMipQueueDepthErrorMessage( sendResponse );

            Console.WriteLine( sendResponse );

            Assert.IsNotNull( sendResponse.Result, "Result" );
            Assert.IsNotNull( sendResponse.InnerResponses, "InnerResponses" );

            Assert_That_Text_Contains( sendResponse, MipOperationStatus.ZipFeedSuccess );
            Assert_That_Text_Contains( sendResponse, MipOperationStatus.SftpSendFilesSuccess );

            var request = new MipFeedSubmissionDescriptor( feed, sendResponse.Result.FeedSubmissionId );
            var findResponse = MipConnector.Instance.FindSubmission( request, MipFeedSubmissionProcessingStatus.InProgress );
            Console.WriteLine();
            Console.WriteLine( findResponse );

            Assert.AreEqual( MipOperationStatus.FindSubmissionSuccess, findResponse.StatusCode );
            Assert.IsNotNull( findResponse.Result.RemoteFileName );
            Assert.IsNotNull( findResponse.Result.RemoteDir );
            Assert.IsTrue( findResponse.Result.RemoteFileName.Length > 1 );
        }

        // --------------------------------------------------------[]
        [Test]
        public void FindRequest_Inprocess_Unknown()
        {
            var feed = new MipFeedDescriptor( MipFeedType.Product );
            var request = new MipFeedSubmissionDescriptor( feed, MipFeedSubmissionDescriptor.GenerateId() );

            var findResponse = MipConnector.Instance.FindSubmission( request, MipFeedSubmissionProcessingStatus.InProgress );
            Console.WriteLine( findResponse );

            Assert.That( findResponse.StatusCode.Equals( MipOperationStatus.FindSubmissionFailure ) );
            Assert.That( findResponse.Result == null );
            Assert_That_Text_Contains( findResponse, MipOperationStatus.FindSubmissionFailure );
            Assert_That_Text_Contains( findResponse, @"not found in [store/product/inprocess]" );
        }

        // --------------------------------------------------------[]
        [Test]
        public void FindRequest_Inprocess_Wrong()
        {
            var feed = new MipFeedDescriptor( MipFeedType.None );
            var request = new MipFeedSubmissionDescriptor( feed, MipFeedSubmissionDescriptor.GenerateId() );

            var findResponse = MipConnector.Instance.FindSubmission( request, MipFeedSubmissionProcessingStatus.InProgress );
            Console.WriteLine( findResponse );

            Assert.AreEqual( MipOperationStatus.FindSubmissionFailure, findResponse.StatusCode );
            Assert.IsNull( findResponse.Result );
            Assert.IsTrue( findResponse.ToString().Contains( "Exception" ) );
        }

        // --------------------------------------------------------[]
        [Test]
        public void FindRequest_Output()
        {
            var fakeMipConnector = EbayMockHelper.GetMipConnectorSendingTestFeed();

            var feed = new MipFeedDescriptor( MipFeedType.Product );
            var sendResponse = fakeMipConnector.SubmitFeed( feed );
            IgnoreMipQueueDepthErrorMessage( sendResponse );

            Console.WriteLine( sendResponse );
            Assert.IsNotNull( sendResponse.Result );

            var request = new MipFeedSubmissionDescriptor( feed, sendResponse.Result.FeedSubmissionId );
            var findResponse = MipConnector.Instance.FindSubmission( request, MipFeedSubmissionProcessingStatus.Done );
            Console.WriteLine( findResponse );

            Assert.AreEqual( MipOperationStatus.FindSubmissionSuccess, findResponse.StatusCode );
            Assert.IsNotNull( findResponse.Result.RemoteFileName );
            Assert.IsNotNull( findResponse.Result.RemoteDir );
            Assert.IsTrue( findResponse.Result.RemoteFileName.Length > 1 );
            Assert.IsTrue( findResponse.Result.RemoteDir.Length > 1 );
        }

        // --------------------------------------------------------[]
        [Test]
        public void FindRequest_Output_Unknown()
        {
            var feed = new MipFeedDescriptor( MipFeedType.Product );
            var request = new MipFeedSubmissionDescriptor( feed, MipFeedSubmissionDescriptor.GenerateId() );

            var findResponse = MipConnector.Instance.FindSubmission( request, MipFeedSubmissionProcessingStatus.Done );

            Console.WriteLine( findResponse );

            Assert.AreEqual( MipOperationStatus.FindSubmissionFailure, findResponse.StatusCode );
            Assert.IsNull( findResponse.Result );
        }

        // --------------------------------------------------------[]
        [Test]
        public void GetRequestStatus_Inproc()
        {
            var feed = new MipFeedDescriptor( MipFeedType.Product );
            var sendResponse = MipConnector.Instance.SubmitFeed( feed );
            IgnoreMipQueueDepthErrorMessage( sendResponse );

            var request = new MipFeedSubmissionDescriptor( feed, sendResponse.Result.FeedSubmissionId );
            var requestResponse = MipConnector.Instance.GetSubmissionStatus( request );
            Console.WriteLine( requestResponse );

            Assert.AreEqual( MipOperationStatus.GetSubmissionStatusSuccess, requestResponse.StatusCode );
            Assert.AreEqual( MipFeedSubmissionResultStatus.Inprocess, requestResponse.Result.MipFeedSubmissionResultStatusCode );
        }

        // --------------------------------------------------------[]
        [Test]
        public void GetRequestStatus_Success()
        {
            var fakeMipConnector = EbayMockHelper.GetMipConnectorIgnoringInprocessAndSendingTestFeed();

            var feed = new MipFeedDescriptor( MipFeedType.Availability );
            var sendResponse = fakeMipConnector.SubmitFeed( feed );
            IgnoreMipQueueDepthErrorMessage( sendResponse );

            Console.WriteLine( sendResponse );
            Assert.IsNotNull( sendResponse.Result );

            var request = new MipFeedSubmissionDescriptor( feed, sendResponse.Result.FeedSubmissionId );
            var requestResponse = fakeMipConnector.GetSubmissionStatus( request );
            Console.WriteLine( requestResponse );

            if( requestResponse.Result.MipFeedSubmissionResultStatusCode != MipFeedSubmissionResultStatus.Success ) {
                Console.WriteLine(
                    "\n\nIt can be 'cause your tests have been not started for a logn period (2-3 days)\n\n" );
            }

            Assert.AreEqual( MipOperationStatus.GetSubmissionStatusSuccess, requestResponse.StatusCode );
            Assert.AreEqual( MipFeedSubmissionResultStatus.Success, requestResponse.Result.MipFeedSubmissionResultStatusCode );
        }

        // --------------------------------------------------------[]
        [Test]
        public void GetRequestStatus_Unknown()
        {
            var feed = new MipFeedDescriptor( MipFeedType.Product );
            var request = new MipFeedSubmissionDescriptor( feed, MipFeedSubmissionDescriptor.GenerateId() );

            var requestResponse = MipConnector.Instance.GetSubmissionStatus( request );
            Console.WriteLine( requestResponse );

            Assert.AreEqual( MipOperationStatus.GetSubmissionStatusSuccess, requestResponse.StatusCode );
            Assert.AreEqual( MipFeedSubmissionResultStatus.Unknown, requestResponse.Result.MipFeedSubmissionResultStatusCode );
        }

        // --------------------------------------------------------[]
        [Test]
        public void Response_Contains_ArgsInfo()
        {
            var feed = new MipFeedDescriptor( MipFeedType.None );
            var request = new MipFeedSubmissionDescriptor( feed, MipFeedSubmissionDescriptor.GenerateId() );

            var requestResponse = MipConnector.Instance.GetSubmissionStatus( request );
            Console.WriteLine( requestResponse );

            Assert_That_Text_Contains( requestResponse, "ArgsInfo" );
        }
    }
}