// Spreadbot (c) 2015 Krokodev
// Spreadbot.Tests.Nunit.Ebay
// MipConnector_Main_Tests.cs

using System;
using NUnit.Framework;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Connector;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Services.Mip.FeedSubmission;
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

            Assert.That( response.IsSuccessful );
            Assert.IsTrue( MipFeedSubmissionDescriptor.VerifySubmissionId( response.Result.FeedSubmissionId ) );
            Assert_That_Text_Contains( response, "InnerResponses" );
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

            var request = new MipFeedSubmissionDescriptor( feed, sendResponse.Result.FeedSubmissionId );
            var findResponse = MipConnector.Instance.FindSubmission( request,
                MipFeedSubmissionProcessingStatus.InProgress );
            Console.WriteLine();
            Console.WriteLine( findResponse );

            Assert.That( findResponse.IsSuccessful );
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

            var findResponse = MipConnector.Instance.FindSubmission( request,
                MipFeedSubmissionProcessingStatus.InProgress );
            Console.WriteLine( findResponse );

            Assert.IsFalse( findResponse.IsSuccessful );
            Assert.IsNull( findResponse.Result );
            Assert_That_Text_Contains( findResponse, @"not found in [store/product/inprocess]" );
        }

        // --------------------------------------------------------[]
        [Test]
        public void FindRequest_Inprocess_Wrong()
        {
            var feed = new MipFeedDescriptor( MipFeedType.None );
            var request = new MipFeedSubmissionDescriptor( feed, MipFeedSubmissionDescriptor.GenerateId() );

            var findResponse = MipConnector.Instance.FindSubmission( request,
                MipFeedSubmissionProcessingStatus.InProgress );
            Console.WriteLine( findResponse );

            Assert.IsFalse( findResponse.IsSuccessful );
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
            var findResponse = MipConnector.Instance.FindSubmission( request, MipFeedSubmissionProcessingStatus.Complete );
            Console.WriteLine( findResponse );

            Assert.That( findResponse.IsSuccessful );
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

            var findResponse = MipConnector.Instance.FindSubmission( request, MipFeedSubmissionProcessingStatus.Complete );

            Console.WriteLine( findResponse );

            Assert.IsFalse( findResponse.IsSuccessful );
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
            var response = MipConnector.Instance.GetFeedSubmissionOverallStatus( request );
            Console.WriteLine( response );

            Assert.That( response.IsSuccessful );
            Assert.AreEqual( MipFeedSubmissionOverallStatus.InProgress, response.Result.MipFeedSubmissionOverallStatus );
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
            var submissionStatusResponse = fakeMipConnector.GetFeedSubmissionOverallStatus( request );
            Console.WriteLine( submissionStatusResponse );

            if( submissionStatusResponse.Result.MipFeedSubmissionOverallStatus
                != MipFeedSubmissionOverallStatus.Success ) {
                Console.WriteLine(
                    "\n\nIt can be 'cause your tests have been not started for a logn period (2-3 days)\n\n" );
            }

            Assert.That( submissionStatusResponse.IsSuccessful );
            Assert.AreEqual( MipFeedSubmissionOverallStatus.Success,
                submissionStatusResponse.Result.MipFeedSubmissionOverallStatus );
        }

        // --------------------------------------------------------[]
        [Test]
        public void GetRequestStatus_Unknown()
        {
            var feed = new MipFeedDescriptor( MipFeedType.Product );
            var request = new MipFeedSubmissionDescriptor( feed, MipFeedSubmissionDescriptor.GenerateId() );

            var submissionStatusResponse = MipConnector.Instance.GetFeedSubmissionOverallStatus( request );
            Console.WriteLine( submissionStatusResponse );

            Assert.That( submissionStatusResponse.IsSuccessful );
            Assert.AreEqual( MipFeedSubmissionOverallStatus.Unknown,
                submissionStatusResponse.Result.MipFeedSubmissionOverallStatus );
        }

        // --------------------------------------------------------[]
        [Test]
        public void Response_Contains_ArgsInfo()
        {
            var feed = new MipFeedDescriptor( MipFeedType.None );
            var request = new MipFeedSubmissionDescriptor( feed, MipFeedSubmissionDescriptor.GenerateId() );

            var response = MipConnector.Instance.GetFeedSubmissionOverallStatus( request );
            Console.WriteLine( response );

            Assert_That_Text_Contains( response, "ArgsInfo" );
        }
    }
}