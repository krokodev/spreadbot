// Spreadbot (c) 2015 Crocodev
// Spreadbot.Tests.Core
// MipConnector_Status_Tests.cs
// romak_000, 2015-03-25 15:25

using System;
using Crocodev.Common.Extensions;
using MoreLinq;
using NUnit.Framework;
using Spreadbot.Core.Channels.Ebay.Mip.Connector;
using Spreadbot.Core.Channels.Ebay.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Request;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.StatusCode;
using Spreadbot.Tests.Core.Common;

// Code: MipConnector_Content_Tests

namespace Spreadbot.Tests.Core.Channels.Ebay.Mip
{
    [TestFixture]
    public class MipConnector_Content_Tests: SpreadbotBaseTest
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
            var request = new MipRequestHandler( feed, MipConnectorTestInitializer.ItemRequestId );
            var testInfo = "{0} ItemId".SafeFormat( feed.Type );

            var requestResponse = MipConnector.Mock_GetRequestStatus( request );
            Assert.AreEqual( MipConnectorTestInitializer.ProductItemId, requestResponse.Result.MipItemId, testInfo );
        }

        // --------------------------------------------------------[]
        private static void TestFeedStatus( MipFeedType mipFeedType, MipRequestStatus mipRequestStatus )
        {
            var wasTested = false;
            var feed = new MipFeedHandler( mipFeedType );

            MipConnectorTestInitializer.TestRequestIds( feed.Type, mipRequestStatus )
                .ForEach( reqId => {
                    var request = new MipRequestHandler( feed, reqId );
                    var requestResponse = MipConnector.Mock_GetRequestStatus( request );
                    var testInfo = "{0}.{1} checking status".SafeFormat( feed.Type, reqId );
                    wasTested = true;

                    Assert.AreEqual( MipOperationStatus.GetRequestStatusSuccess, requestResponse.Code, testInfo );
                    Assert.AreEqual( mipRequestStatus, requestResponse.Result.MipRequestStatusCode, testInfo );
                } );

            Assert.AreEqual( true, wasTested, "{0}.{1} was not tested".SafeFormat( mipFeedType, mipRequestStatus ) );
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