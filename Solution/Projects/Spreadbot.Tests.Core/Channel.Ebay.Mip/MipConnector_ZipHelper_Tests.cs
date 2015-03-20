// Spreadbot (c) 2015 Crocodev
// Spreadbot.Tests.Core
// MipConnector_ZipHelper_Tests.cs
// romak_000, 2015-03-20 13:57

using System.Diagnostics;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spreadbot.Core.Channels.Ebay.Mip.Connector;
using Spreadbot.Core.Channels.Ebay.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Request;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.StatusCode;

namespace Spreadbot.Tests.Core.Channel.Ebay.Mip
{
    [TestClass]
    public class MipConnector_ZipHelper_Tests
    {
        // ===================================================================================== []
        [TestMethod]
        public void ZipFeed()
        {
            var feed = new MipFeed( MipFeedType.Product );
            var reqId = MipRequest.GenerateTestId().ToString();

            var response = MipConnector.ZipHelper.ZipFeed( feed.Name, reqId );
            Trace.TraceInformation( response.Autoinfo );

            Assert.AreEqual( MipStatusCode.ZipFeedSuccess, response.Code );
            Assert.IsTrue( File.Exists( response.Result.ZipFileName ) );
            Assert.AreEqual( MipStatusCode.ZipFeedSuccess, response.Code );
        }
    }
}