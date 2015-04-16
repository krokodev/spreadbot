// Spreadbot (c) 2015 Crocodev
// Spreadbot.Tests.Nunit.Ebay
// MipConnector_Zip_Tests.cs

using System;
using System.IO;
using NUnit.Framework;
using Spreadbot.Core.Channels.Ebay.Mip.Connector;
using Spreadbot.Core.Channels.Ebay.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Request;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.StatusCode;
using Spreadbot.Nunit.Ebay.Base;

namespace Spreadbot.Nunit.Ebay.Tests
{
    [TestFixture]
    public class MipConnectorZipEbayTests : SpreadbotEbayTestBase
    {
        // --------------------------------------------------------[]
        [SetUp]
        public static void Init()
        {
            MipConnectorTestInitializer.PrepareTestFiles();
        }

        // --------------------------------------------------------[]
        [Test]
        public void ZipFeed()
        {
            var feed = new MipFeedHandler( MipFeedType.Product );
            var reqId = MipRequestHandler.GenerateZeroId();

            var response = MipConnector.Instance.ZipHelper.ZipFeed( feed, reqId );
            Console.WriteLine( response );

            Assert.AreEqual( MipOperationStatus.ZipFeedSuccess, response.StatusCode );
            Assert.IsTrue( File.Exists( response.Result.ZipFileName ) );
            Assert.AreEqual( MipOperationStatus.ZipFeedSuccess, response.StatusCode );
        }
    }
}