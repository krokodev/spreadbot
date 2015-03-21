﻿// Spreadbot (c) 2015 Crocodev
// Spreadbot.Tests.Core
// MipConnector_SftpHelper_Tests.cs
// romak_000, 2015-03-21 2:11

using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spreadbot.Core.Channels.Ebay.Mip.Connector;
using Spreadbot.Core.Channels.Ebay.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Request;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.StatusCode;

namespace Spreadbot.Tests.Core.Channels.Ebay.Mip
{
    [TestClass]
    public class MipConnector_SftpHelper_Tests
    {
        // ===================================================================================== []
        [ClassInitialize]
        public static void Init( TestContext testContext )
        {
            MipTestInitializer.PrepareTestFiles();
        }

        // ===================================================================================== []
        [TestMethod]
        public void SendZippedFeed()
        {
            var feed = new MipFeedHandler( MipFeedType.Product );
            var response = MipConnector.SftpHelper.SendZippedFeed( feed.GetName(), MipRequest.GenerateTestId() );

            Trace.TraceInformation( response.Autoinfo );

            Assert.AreEqual( MipStatusCode.SendZippedFeedSuccess, response.Code );
        }

        // ===================================================================================== []
        [TestMethod]
        public void TestConnection_Good()
        {
            var response = MipConnector.SftpHelper.TestConnection();

            Trace.TraceInformation( response.Autoinfo );

            Assert.AreEqual( MipStatusCode.TestConnectionSuccess, response.Code );
        }

        // ===================================================================================== []
        [TestMethod]
        public void TestConnection_Bad()
        {
            var response = MipConnector.SftpHelper.TestConnection( "wrong password" );

            Trace.TraceInformation( response.Autoinfo );

            Assert.AreEqual( MipStatusCode.TestConnectionFail, response.Code );
        }
    }
}