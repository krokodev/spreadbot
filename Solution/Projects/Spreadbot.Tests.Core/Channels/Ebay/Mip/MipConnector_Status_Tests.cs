// Spreadbot (c) 2015 Crocodev
// Spreadbot.Tests.Core
// MipConnector_Status_Tests.cs
// romak_000, 2015-03-25 14:07

using System;
using MoreLinq;
using NUnit.Framework;
using Spreadbot.Core.Channels.Ebay.Mip.Connector;
using Spreadbot.Core.Channels.Ebay.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Request;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.StatusCode;

// Code: MipConnector_Content_Tests

namespace Spreadbot.Tests.Core.Channels.Ebay.Mip
{
    [TestFixture]
    public class MipConnector_Content_Tests
    {
        // --------------------------------------------------------[]
        [SetUp]
        public void Init()
        {
            MipConnectorTestInitializer.PrepareTestFiles();
        }

        // --------------------------------------------------------[]
        private static void TestItemId( MipFeedType mipFeedType )
        {
            var feed = new MipFeedHandler( mipFeedType );
            var request = new MipRequestHandler( feed, MipConnectorTestInitializer.ItemRequestIds );

            var requestResponse = MipConnector.Mock_GetRequestStatus( request );
            Console.WriteLine( "Result.MipItemId: [{0}]", requestResponse.Result.MipItemId );
            Console.WriteLine( requestResponse.Autoinfo );
            Assert.AreEqual( MipConnectorTestInitializer.ProductItemId, requestResponse.Result.MipItemId );
        }

        // --------------------------------------------------------[]
        private static void TestFeedStatus( MipFeedType mipFeedType, MipRequestStatus mipRequestStatus )
        {
            var wasVerified = false;

            var feed = new MipFeedHandler( mipFeedType );

            MipConnectorTestInitializer.TestRequestIds( feed.Type, mipRequestStatus )
                .ForEach( reqId => {
                    var request = new MipRequestHandler( feed, reqId );
                    var requestResponse = MipConnector.Mock_GetRequestStatus( request );
                    wasVerified = true;

                    Console.WriteLine( "\n\n" );
                    Console.WriteLine( "Feed: [{0}]", mipFeedType);
                    Console.WriteLine( "RequestID: [{0}]", reqId );
                    Console.WriteLine( "Result.MipRequestStatusCode: [{0}]", requestResponse.Result.MipRequestStatusCode );
                   // Console.WriteLine( requestResponse.Autoinfo );

                    Assert.AreEqual( MipOperationStatus.GetRequestStatusSuccess, requestResponse.Code );
                    Assert.AreEqual( mipRequestStatus, requestResponse.Result.MipRequestStatusCode );
                } );

            Assert.AreEqual( true, wasVerified );
        }

        // --------------------------------------------------------[]
        private static void TestAllFeedStatuses( MipFeedType mipFeedType )
        {
            TestFeedStatus( mipFeedType, MipRequestStatus.Success );
            TestFeedStatus( mipFeedType, MipRequestStatus.Failure );
            TestFeedStatus( mipFeedType, MipRequestStatus.Unknown );
        }

        // --------------------------------------------------------[]
        [Test]
        public void Read_Product_Content()
        {
            TestItemId( MipFeedType.Product );
            TestAllFeedStatuses( MipFeedType.Product );
        }

        // --------------------------------------------------------[]
        [Test]
        public void Read_Availability_Content()
        {
            TestAllFeedStatuses( MipFeedType.Availability );
        }

        // --------------------------------------------------------[]
        [Test]
        public void Read_Distribution_Content()
        {
            TestItemId( MipFeedType.Distribution );
            TestAllFeedStatuses( MipFeedType.Distribution );
        }
    }
}