// Spreadbot (c) 2015 Crocodev
// Tests.NUnit
// MipConnector_Content_Tests.cs
// Roman, 2015-04-06 3:09 PM

using System;
using Crocodev.Common.Extensions;
using Moq;
using NUnit.Framework;
using Spreadbot.Core.Channels.Ebay.Configuration.Sections;
using Spreadbot.Core.Channels.Ebay.Mip.Connector;
using Spreadbot.Core.Channels.Ebay.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Request;
using Tests.NUnit.Code;

namespace Tests.NUnit.Units
{
    [TestFixture]
    public class MipConnector_Content_Tests : SpreadbotTestBase
    {
        // --------------------------------------------------------[]
        [SetUp]
        public void InitMethod()
        {
            MipConnectorTestInitializer.PrepareTestFiles();
        }

        // --------------------------------------------------------[]
        private static void _TestItemId( MipFeedType mipFeedType, IMipConnector mipConnector )
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
        private static void TestItemId( MipFeedType mipFeedType )
        {
            var mipConnector = MipConnector.Instance;

            var mockMipConnector = new Mock< MipConnector > { CallBase = true };

            _TestItemId( mipFeedType, mockMipConnector.Object );
        }

        // --------------------------------------------------------[]
        [Ignore]
        [Test]
        public void Read_ItemId()
        {
            TestItemId( MipFeedType.Distribution );
        }

        // --------------------------------------------------------[]
        [Test]
        public void Read_Mip_Config()
        {
            var configuration = MipPublicConfig.Instance;
            Assert.AreEqual( "mip.ebay.com", configuration.MipConnection.HostName );
            Assert.AreEqual( 22, configuration.MipConnection.PortNumber );
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
        /*        private static void TestAllFeedStatuses( MipFeedType mipFeedType )
        {
            TestFeedStatus( mipFeedType, MipRequestStatus.Success );
            TestFeedStatus( mipFeedType, MipRequestStatus.Failure );
            TestFeedStatus( mipFeedType, MipRequestStatus.Unknown );
        }

        // --------------------------------------------------------[]
        [NUnit.Framework.Ignore( "Waiting for Fakes" )]
        [Test]
        public void Read_Product_Content()
        {
            TestItemId( MipFeedType.Product );
            TestAllFeedStatuses( MipFeedType.Product );
        }

        // --------------------------------------------------------[]
        [NUnit.Framework.Ignore( "Waiting for Fakes" )]
        [Test]
        public void Read_Availability_Content()
        {
            TestAllFeedStatuses( MipFeedType.Availability );
        }

        // --------------------------------------------------------[]
        [NUnit.Framework.Ignore( "Waiting for Fakes" )]
        [Test]
        public void Read_Distribution_Content()
        {
            TestItemId( MipFeedType.Distribution );
            TestAllFeedStatuses( MipFeedType.Distribution );
        }*/
    }
}