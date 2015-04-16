// Spreadbot (c) 2015 Crocodev
// Spreadbot.Tests.NUnit
// MipConnector_Zip_Tests.cs

using System;
using System.IO;
using NUnit.Framework;
using Spreadbot.Core.Channels.Ebay.Mip.Connector;
using Spreadbot.Core.Channels.Ebay.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Request;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.StatusCode;
using Spreadbot.Tests.NUnit.Code;

namespace Spreadbot.Tests.NUnit.Units
{
    [TestFixture]
    public class MipConnector_Zip_Tests : SpreadbotTestBase
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