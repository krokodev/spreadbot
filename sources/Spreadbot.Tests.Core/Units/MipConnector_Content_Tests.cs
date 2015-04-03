// Spreadbot (c) 2015 Crocodev
// Spreadbot.Tests.Core
// MipConnector_Content_Tests.cs
// Roman, 2015-04-03 5:09 PM

using Crocodev.Common.Extensions;
using Microsoft.QualityTools.Testing.Fakes;
using NUnit.Framework;
using Spreadbot.Core.Channels.Ebay.Mip.Connector;
using Spreadbot.Core.Channels.Ebay.Mip.Connector.Fakes;
using Spreadbot.Core.Channels.Ebay.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Request;
using Spreadbot.Tests.Core.Code;

namespace Spreadbot.Tests.Core.Units
{
    [TestFixture]
    public class MipConnector_Content_Tests : SpreadbotTestBase
    {
        // --------------------------------------------------------[]
        [SetUp]
        public void Init()
        {
            MipConnectorTestInitializer.PrepareTestFiles();
        }

        // --------------------------------------------------------[]
        // Code: Use Fakes
        private static void DoTestItemId( MipFeedType mipFeedType )
        {
            var feed = new MipFeedHandler( mipFeedType );
            var request = new MipRequestHandler( feed, MipConnectorTestInitializer.ItemRequestId );
            var testInfo = "{0} ItemId".SafeFormat( feed.Type );

            var requestResponse = MipConnector.GetRequestStatus( request );
            Assert.AreEqual( MipConnectorTestInitializer.ProductItemId, requestResponse.Result.MipItemId, testInfo );
        }

        // --------------------------------------------------------[]
        private static void TestItemId( MipFeedType mipFeedType )
        {
            using( ShimsContext.Create() ) {
                ShimMipConnector
                    .GetRequestStatusMipRequestHandlerBoolean =
                    ( mipRequestHandler, b ) =>
                        null;

                DoTestItemId( mipFeedType );
            }
        }

        // --------------------------------------------------------[]
        [Test]
        public void Read_ItemId()
        {
            TestItemId( MipFeedType.Distribution );
        }

        // --------------------------------------------------------[]
        private static void TestFeedStatus( MipFeedType mipFeedType, MipRequestStatus mipRequestStatus )
        {
            /*            var wasTested = false;
            var feed = new MipFeedHandler( mipFeedType );

            MipConnectorTestInitializer.TestRequestIds( feed.Type, mipRequestStatus )
                .ForEach( reqId => {
                    var request = new MipRequestHandler( feed, reqId );
                    var requestResponse = MipConnector.Mock_GetRequestStatus( request );
                    var testInfo = "{0}.{1} checking status".SafeFormat( feed.Type, reqId );
                    wasTested = true;

                    Assert.AreEqual( MipOperationStatus.GetRequestStatusSuccess, requestResponse.StatusCode, testInfo );
                    Assert.AreEqual( mipRequestStatus, requestResponse.Result.MipRequestStatusCode, testInfo );
                } );

            Assert.AreEqual( true, wasTested, "{0}.{1} was not tested".SafeFormat( mipFeedType, mipRequestStatus ) );*/
            Assert.Fail();
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