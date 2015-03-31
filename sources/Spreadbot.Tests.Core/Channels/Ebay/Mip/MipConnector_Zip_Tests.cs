// Spreadbot (c) 2015 Crocodev
// Spreadbot.Tests.Core
// MipConnector_Zip_Tests.cs
// Roman, 2015-03-31 1:27 PM

using System;
using System.IO;
using NUnit.Framework;
using Spreadbot.Core.Channels.Ebay.Mip.Connector;
using Spreadbot.Core.Channels.Ebay.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Request;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.StatusCode;
using Spreadbot.Tests.Core.Common;

namespace Spreadbot.Tests.Core.Channels.Ebay.Mip
{
    [TestFixture]
    public class MipConnector_Zip_Tests : SpreadbotBaseTest
    {
        // ===================================================================================== []
        [SetUp]
        public static void Init()
        {
            MipConnectorTestInitializer.PrepareTestFiles();
        }

        // ===================================================================================== []
        [Test]
        public void ZipFeed()
        {
            var feed = new MipFeedHandler( MipFeedType.Product );
            var reqId = MipRequestHandler.GenerateTestId();

            var response = MipConnector.ZipHelper.ZipFeed( feed.GetName(), reqId );
            Console.WriteLine( response );

            Assert.AreEqual( MipOperationStatus.ZipFeedSuccess, response.Code );
            Assert.IsTrue( File.Exists( response.Result.ZipFileName ) );
            Assert.AreEqual( MipOperationStatus.ZipFeedSuccess, response.Code );
        }
    }
}