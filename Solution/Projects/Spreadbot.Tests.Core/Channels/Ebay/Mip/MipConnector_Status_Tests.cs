// Spreadbot (c) 2015 Crocodev
// Spreadbot.Tests.Core
// MipConnector_Status_Tests.cs
// romak_000, 2015-03-24 11:40

using System;
using NUnit.Framework;
using Spreadbot.Core.Channels.Ebay.Mip.Connector;
using Spreadbot.Core.Channels.Ebay.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Request;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.StatusCode;

namespace Spreadbot.Tests.Core.Channels.Ebay.Mip
{
    // Code: MipConnector_Status_Tests
    [TestFixture]
    public class MipConnector_Status_Tests
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
            var request = new MipRequestHandler( feed, MipConnectorTestInitializer.ProductSuccessRequestId );

            var requestResponse = MipConnector.Mock_GetRequestStatus( request );
            Console.WriteLine( requestResponse.Autoinfo );

            Assert.AreEqual( MipStatusCode.GetRequestStatusSuccess, requestResponse.Code );
            Assert.AreEqual( MipRequestStatus.Success, requestResponse.Result.MipRequestStatusCode );
        }
    }
}