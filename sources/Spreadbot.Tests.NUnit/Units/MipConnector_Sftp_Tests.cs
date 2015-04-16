// Spreadbot (c) 2015 Crocodev
// Spreadbot.Tests.NUnit
// MipConnector_Sftp_Tests.cs

using System;
using NUnit.Framework;
using Spreadbot.Core.Channels.Ebay.Mip.Connector;
using Spreadbot.Core.Channels.Ebay.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Request;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.StatusCode;
using Spreadbot.Tests.NUnit.Code;

namespace Spreadbot.Tests.NUnit.Units
{
    [TestFixture]
    public class MipConnector_Sftp_Tests : SpreadbotTestBase
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

            var reqId = MipRequestHandler.GenerateZeroId();
            var localFiles = MipConnector.LocalZippedFeedFile( feed.GetName(), reqId );
            var remoteFiles = MipConnector.RemoteFeedOutgoingZipFilePath( feed.GetName(), reqId );

            var response = MipConnector.Instance.SftpHelper.SendFiles( localFiles, remoteFiles );
            IgnoreMipQueueDepthErrorMessage( response.ToString() );

            Console.WriteLine( response );

            Assert.AreEqual( MipOperationStatus.SftpSendFilesSuccess, response.StatusCode );
        }

        // ===================================================================================== []
        [Test]
        public void TestConnection_Good()
        {
            var response = MipConnector.Instance.SftpHelper.TestConnection();

            Console.WriteLine( response );

            Assert.AreEqual( MipOperationStatus.TestConnectionSuccess, response.StatusCode );
        }

        // ===================================================================================== []
        [Test]
        public void TestConnection_Bad()
        {
            var response = MipConnector.Instance.SftpHelper.TestConnection( "wrong password" );

            Console.WriteLine( response );

            Assert.AreEqual( MipOperationStatus.TestConnectionFailure, response.StatusCode );
        }
    }
}