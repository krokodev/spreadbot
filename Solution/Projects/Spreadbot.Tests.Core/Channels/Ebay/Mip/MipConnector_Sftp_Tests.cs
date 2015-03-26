// Spreadbot (c) 2015 Crocodev
// Spreadbot.Tests.Core
// MipConnector_Sftp_Tests.cs
// romak_000, 2015-03-25 15:24

using System;
using NUnit.Framework;
using Spreadbot.Core.Channels.Ebay.Mip.Connector;
using Spreadbot.Core.Channels.Ebay.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Request;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.StatusCode;
using Spreadbot.Tests.Core.Common;

namespace Spreadbot.Tests.Core.Channels.Ebay.Mip
{
    [TestFixture]
    public class MipConnector_Sftp_Tests: SpreadbotBaseTest
    {
        // ===================================================================================== []
        [SetUp]
        public static void Init()
        {
            MipConnectorTestInitializer.PrepareTestFiles();
        }

        // ===================================================================================== []
        [Test]
        public void SendZippedFeed()
        {
            var feed = new MipFeedHandler( MipFeedType.Product );
            var response = MipConnector.SftpHelper.SendZippedFeed( feed.GetName(), MipRequestHandler.GenerateTestId() );

            Console.WriteLine( response );

            Assert.AreEqual( MipOperationStatus.SendZippedFeedSuccess, response.Code );
        }

        // ===================================================================================== []
        [Test]
        public void TestConnection_Good()
        {
            var response = MipConnector.SftpHelper.TestConnection();

            Console.WriteLine( response );

            Assert.AreEqual( MipOperationStatus.TestConnectionSuccess, response.Code );
        }

        // ===================================================================================== []
        [Test]
        public void TestConnection_Bad()
        {
            var response = MipConnector.SftpHelper.TestConnection( "wrong password" );

            Console.WriteLine( response );

            Assert.AreEqual( MipOperationStatus.TestConnectionFailure, response.Code );
        }
    }
}