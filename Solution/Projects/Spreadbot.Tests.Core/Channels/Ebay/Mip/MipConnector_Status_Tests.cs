// Spreadbot (c) 2015 Crocodev
// Spreadbot.Tests.Core
// MipConnector_Status_Tests.cs
// romak_000, 2015-03-25 13:12

using System;
using MoreLinq;
using NUnit.Framework;
using Spreadbot.Core.Channels.Ebay.Mip.Connector;
using Spreadbot.Core.Channels.Ebay.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Request;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.StatusCode;

namespace Spreadbot.Tests.Core.Channels.Ebay.Mip
{
    // Code: MipConnector_Content_Tests
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
        [Test]
        public void Read_Product_Status_Success()
        {
            var feed = new MipFeedHandler( MipFeedType.Product );

            MipConnectorTestInitializer.TestRequestIds( feed.Type, MipRequestStatus.Success )
                .ForEach( reqId => {
                    var request = new MipRequestHandler( feed, reqId );

                    var requestResponse = MipConnector.Mock_GetRequestStatus( request );
                    Console.WriteLine( "\n\n");
                    Console.WriteLine( "RequestID: [{0}]", reqId);
                    Console.WriteLine( "Result.MipRequestStatusCode: [{0}]", requestResponse.Result.MipRequestStatusCode );
                    Console.WriteLine( requestResponse.Autoinfo );

                    Assert.AreEqual( MipOperationStatus.GetRequestStatusSuccess, requestResponse.Code );
                    Assert.AreEqual( MipRequestStatus.Success, requestResponse.Result.MipRequestStatusCode );
                } );
        }

        // --------------------------------------------------------[]
        [Test]
        public void Read_Product_Item()
        {
            var feed = new MipFeedHandler( MipFeedType.Product );

            MipConnectorTestInitializer.TestRequestIds( feed.Type, MipRequestStatus.Success )
                .ForEach( reqId => {
                    var request = new MipRequestHandler( feed, reqId );

                    var requestResponse = MipConnector.Mock_GetRequestStatus( request );
                    Console.WriteLine( "Result.MipItemId: [{0}]", requestResponse.Result.MipItemId );
                    Console.WriteLine( requestResponse.Autoinfo );

                    Assert.AreEqual( MipConnectorTestInitializer.ProductItemId, requestResponse.Result.MipItemId );
                } );
        }

        // --------------------------------------------------------[]
        [Ignore( "Not ready" )]
        [Test]
        public void Read_Product_Status_Fail_On_Mip()
        {
            var feed = new MipFeedHandler( MipFeedType.Product );

            MipConnectorTestInitializer.TestRequestIds( feed.Type, MipRequestStatus.Success )
                .ForEach( reqId => {
                    var request = new MipRequestHandler( feed, reqId );
                    var requestResponse = MipConnector.Mock_GetRequestStatus( request );
                    Console.WriteLine( "Result.MipItemId: [{0}]", requestResponse.Result.MipItemId );
                    Console.WriteLine( requestResponse.Autoinfo );

                    Assert.AreEqual( MipConnectorTestInitializer.ProductItemId, requestResponse.Result.MipItemId );
                } );
        }

        // --------------------------------------------------------[]
        [Ignore( "Not ready" )]
        [Test]
        public void Read_Availability_Status_Success() {}

        // --------------------------------------------------------[]
        [Ignore( "Not ready" )]
        [Test]
        public void Read_Availability_Status_Fail() {}

        // --------------------------------------------------------[]
        [Ignore( "Not ready" )]
        [Test]
        public void Read_Distribution_Status_Success() {}

        // --------------------------------------------------------[]
        [Ignore( "Not ready" )]
        [Test]
        public void Read_Disrtibution_Status_Fail() {}
    }
}