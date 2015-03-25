// Spreadbot (c) 2015 Crocodev
// Spreadbot.Tests.Core
// MipConnector_Zip_Tests.cs
// romak_000, 2015-03-25 15:25

using System.Diagnostics;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spreadbot.Core.Channels.Ebay.Mip.Connector;
using Spreadbot.Core.Channels.Ebay.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Request;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.StatusCode;

namespace Spreadbot.Tests.Core.Channels.Ebay.Mip
{
    [TestClass]
    public class MipConnector_Zip_Tests
    {
        // ===================================================================================== []
        [ClassInitialize]
        public static void Init( TestContext testContext )
        {
            MipConnectorTestInitializer.PrepareTestFiles();
        }

        // ===================================================================================== []
        [TestMethod]
        public void ZipFeed()
        {
            var feed = new MipFeedHandler( MipFeedType.Product );
            var reqId = MipRequestHandler.GenerateTestId();

            var response = MipConnector.ZipHelper.ZipFeed( feed.GetName(), reqId );
            Trace.TraceInformation( response.Autoinfo );

            Assert.AreEqual( MipOperationStatus.ZipFeedSuccess, response.Code );
            Assert.IsTrue( File.Exists( response.Result.ZipFileName ) );
            Assert.AreEqual( MipOperationStatus.ZipFeedSuccess, response.Code );
        }
    }
}