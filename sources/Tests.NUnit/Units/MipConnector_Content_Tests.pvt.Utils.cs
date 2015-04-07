// Spreadbot (c) 2015 Crocodev
// Tests.NUnit
// MipConnector_Content_Tests.pvt.Utils.cs
// Roman, 2015-04-07 12:13 PM

using System;
using Crocodev.Common.Extensions;
using MoreLinq;
using NUnit.Framework;
using Spreadbot.Core.Channels.Ebay.Mip.Connector;
using Spreadbot.Core.Channels.Ebay.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Request;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.StatusCode;
using Tests.NUnit.Code;
using Tests.NUnit.Mocks;

namespace Tests.NUnit.Units
{
    [TestFixture]
    public partial class MipConnector_Content_Tests
    {
        // --------------------------------------------------------[]
        private static void TestReadItemId( MipFeedType mipFeedType, IMipConnector mipConnector )
        {
            var feed = new MipFeedHandler( mipFeedType );
            var request = new MipRequestHandler( feed, MipConnectorTestInitializer.ItemRequestId );

            var requestResponse = mipConnector.GetRequestStatus( request );
            Console.WriteLine( requestResponse );

            Assert.IsNotNull( requestResponse.Result );
            Assert.AreEqual( MipConnectorTestInitializer.ProductItemId,
                requestResponse.Result.MipItemId,
                "{0}.ItemId".SafeFormat( feed.Type ) );
        }

        // --------------------------------------------------------[]
        private static void TestReadItemId( MipFeedType mipFeedType )
        {
            TestReadItemId( mipFeedType, MockHelper.GetMipConnectorForUsingLocalData() );
        }

        // --------------------------------------------------------[]
        private static void TestReadFeedStatus(
            MipFeedType mipFeedType,
            MipRequestStatus mipRequestStatus,
            IMipConnector mipConnector )
        {
            var wasTested = false;
            var feed = new MipFeedHandler( mipFeedType );

            MipConnectorTestInitializer.TestRequestIds( feed.Type, mipRequestStatus )
                .ForEach( reqId => {
                    var request = new MipRequestHandler( feed, reqId );
                    var requestResponse = mipConnector.GetRequestStatus( request );
                    var testInfo = "{0}.{1} checking status".SafeFormat( feed.Type, reqId );
                    wasTested = true;

                    Assert.AreEqual( MipOperationStatus.GetRequestStatusSuccess, requestResponse.StatusCode, testInfo );
                    Assert.AreEqual( mipRequestStatus, requestResponse.Result.MipRequestStatusCode, testInfo );
                } );

            Assert.AreEqual( true, wasTested, "{0}.{1} was not tested".SafeFormat( mipFeedType, mipRequestStatus ) );
        }

        // --------------------------------------------------------[]
        private static void TestReadAllFeedStatuses( MipFeedType mipFeedType )
        {
            var fakeMipConnector = MockHelper.GetMipConnectorForUsingLocalData();

            TestReadFeedStatus( mipFeedType, MipRequestStatus.Success, fakeMipConnector );
            TestReadFeedStatus( mipFeedType, MipRequestStatus.Failure, fakeMipConnector );
            TestReadFeedStatus( mipFeedType, MipRequestStatus.Unknown, fakeMipConnector );
        }
    }
}