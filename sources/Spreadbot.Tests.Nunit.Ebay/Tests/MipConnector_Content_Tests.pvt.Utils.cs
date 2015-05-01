// Spreadbot (c) 2015 Krokodev
// Spreadbot.Tests.Nunit.Ebay
// MipConnector_Content_Tests.pvt.Utils.cs

using System;
using Krokodev.Common.Extensions;
using MoreLinq;
using NUnit.Framework;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Connector;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Operations.FeedSubmission;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Operations.Statuses;
using Spreadbot.Nunit.Ebay.Mocks;
using Spreadbot.Nunit.Ebay.Utils;

namespace Spreadbot.Nunit.Ebay.Tests
{
    [TestFixture]
    public partial class MipConnectorContent_Tests
    {
        // --------------------------------------------------------[]
        private static void _TestReadItemId( MipFeedType mipFeedType, IMipConnector mipConnector )
        {
            var feed = new MipFeedDescriptor( mipFeedType );
            var request = new MipFeedSubmissionDescriptor( feed, MipConnectorTestInitializer.ItemRequestId );

            var requestResponse = mipConnector.GetSubmissionStatus( request );
            Console.WriteLine( requestResponse );

            Assert.IsNotNull( requestResponse.Result );
            Assert.AreEqual( MipConnectorTestInitializer.ProductItemId,
                requestResponse.Result.MipItemId,
                "{0}.ItemId".SafeFormat( feed.Type ) );
        }

        // --------------------------------------------------------[]
        private static void _TestReadItemId( MipFeedType mipFeedType )
        {
            _TestReadItemId( mipFeedType, EbayMockHelper.GetMipConnectorUsingLocalData() );
        }

        // --------------------------------------------------------[]
        private static void TestReadFeedStatus(
            MipFeedType mipFeedType,
            MipFeedSubmissionResultStatus mipFeedSubmissionResultStatus,
            IMipConnector mipConnector )
        {
            var wasTested = false;
            var feed = new MipFeedDescriptor( mipFeedType );

            MipConnectorTestInitializer.TestRequestIds( feed.Type, mipFeedSubmissionResultStatus )
                .ForEach( reqId => {
                    var request = new MipFeedSubmissionDescriptor( feed, reqId );
                    var requestResponse = mipConnector.GetSubmissionStatus( request );
                    var testInfo = "{0}.{1} checking status".SafeFormat( feed.Type, reqId );
                    wasTested = true;

                    Assert.AreEqual( MipOperationStatus.GetSubmissionStatusSuccess, requestResponse.StatusCode, testInfo );
                    Assert.AreEqual( mipFeedSubmissionResultStatus, requestResponse.Result.MipFeedSubmissionResultStatusCode, testInfo );
                } );

            Assert.AreEqual( true, wasTested, "{0}.{1} was not tested".SafeFormat( mipFeedType, mipFeedSubmissionResultStatus ) );
        }

        // --------------------------------------------------------[]
        private static void _TestReadAllFeedStatuses( MipFeedType mipFeedType )
        {
            var fakeMipConnector = EbayMockHelper.GetMipConnectorUsingLocalData();

            TestReadFeedStatus( mipFeedType, MipFeedSubmissionResultStatus.Success, fakeMipConnector );
            TestReadFeedStatus( mipFeedType, MipFeedSubmissionResultStatus.Failure, fakeMipConnector );
            TestReadFeedStatus( mipFeedType, MipFeedSubmissionResultStatus.Unknown, fakeMipConnector );
        }
    }
}