// Spreadbot (c) 2015 Crocodev
// Spreadbot.Tests.Core
// MipConnectorSftp_Tests.cs
// Roman, 2015-04-03 1:45 PM

using System;
using NUnit.Framework;
using Spreadbot.Core.Channels.Ebay.Mip.Connector;
using Spreadbot.Core.Channels.Ebay.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Request;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.StatusCode;
using Spreadbot.Tests.Core.Code;

namespace Spreadbot.Tests.Core.Units
{
    [TestFixture]
    public class MipConnectorSftp_Tests : SpreadbotTestBase
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

            var reqId = MipRequestHandler.GenerateTestId();
            var localFiles = MipConnector.LocalZippedFeedFile( feed.GetName(), reqId );
            var remoteFiles = MipConnector.RemoteFeedOutgoingZipFilePath( feed.GetName(), reqId );

            var response = MipConnector.SftpHelper.SendFiles( localFiles, remoteFiles );
            IgnoreMipQueueDepthErrorMessage( response.ToString() );

            Console.WriteLine( response );

            Assert.AreEqual( MipOperationStatus.SftpSendFilesSuccess, response.StatusCode );
        }

        // ===================================================================================== []
        [Test]
        public void TestConnection_Good()
        {
            var response = MipConnector.SftpHelper.TestConnection();

            Console.WriteLine( response );

            Assert.AreEqual( MipOperationStatus.TestConnectionSuccess, response.StatusCode );
        }

        // ===================================================================================== []
        [Test]
        public void TestConnection_Bad()
        {
            var response = MipConnector.SftpHelper.TestConnection( "wrong password" );

            Console.WriteLine( response );

            Assert.AreEqual( MipOperationStatus.TestConnectionFailure, response.StatusCode );
        }
    }
}