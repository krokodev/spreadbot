// Spreadbot (c) 2015 Krokodev
// Spreadbot.Tests.Nunit.Ebay
// MipConnector_Content_Tests.pvt.Utils.cs

using System;
using Krokodev.Common.Extensions;
using MoreLinq;
using NUnit.Framework;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Connector;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Services.Mip.FeedSubmission;
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

            var response = mipConnector.GetSubmissionStatus( request );
            Console.WriteLine( response );

            Assert.IsNotNull( response.Result );
            Assert.AreEqual( MipConnectorTestInitializer.ProductItemId,
                response.Result.MipItemId,
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
            MipFeedSubmissionStatus mipFeedSubmissionStatus,
            IMipConnector mipConnector )
        {
            var wasTested = false;
            var feed = new MipFeedDescriptor( mipFeedType );

            MipConnectorTestInitializer.TestRequestIds( feed.Type, mipFeedSubmissionStatus )
                .ForEach( reqId => {
                    var request = new MipFeedSubmissionDescriptor( feed, reqId );
                    var response = mipConnector.GetSubmissionStatus( request );
                    var testInfo = "{0}.{1} checking status".SafeFormat( feed.Type, reqId );
                    wasTested = true;

                    Assert.That( response.IsSuccessful, testInfo );
                    Assert.AreEqual( mipFeedSubmissionStatus,
                        response.Result.MipFeedSubmissionStatus,
                        testInfo );
                } );

            Assert.AreEqual( true,
                wasTested,
                "{0}.{1} was not tested".SafeFormat( mipFeedType, mipFeedSubmissionStatus ) );
        }

        // --------------------------------------------------------[]
        private static void _TestReadAllFeedStatuses( MipFeedType mipFeedType )
        {
            var fakeMipConnector = EbayMockHelper.GetMipConnectorUsingLocalData();

            TestReadFeedStatus( mipFeedType, MipFeedSubmissionStatus.Success, fakeMipConnector );
            TestReadFeedStatus( mipFeedType, MipFeedSubmissionStatus.Failure, fakeMipConnector );
            TestReadFeedStatus( mipFeedType, MipFeedSubmissionStatus.Unknown, fakeMipConnector );
        }
    }
}