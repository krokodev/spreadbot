﻿// Spreadbot (c) 2015 Krokodev
// Spreadbot.Tests.Nunit.Ebay
// MipConnector_Sftp_Tests.cs

using System;
using NUnit.Framework;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Connector;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Services.Mip.FeedSubmission;
using Spreadbot.Nunit.Ebay.Base;
using Spreadbot.Nunit.Ebay.Utils;

namespace Spreadbot.Nunit.Ebay.Tests
{
    [TestFixture]
    public class MipConnector_Sftp_Tests : Ebay_Tests
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
            var feed = new MipFeedDescriptor( MipFeedType.Product );

            var reqId = MipFeedSubmissionDescriptor.GenerateZeroId();
            var localFiles = MipConnector.LocalZippedFeedFile( feed.GetName(), reqId );
            var remoteFiles = MipConnector.RemoteFeedOutgoingZipFilePath( feed.GetName(), reqId );

            var response = MipConnector.Instance.SftpHelper.SendFiles( localFiles, remoteFiles );
            IgnoreMipQueueDepthErrorMessage( response.ToString() );

            Console.WriteLine( response );

            Assert.That( response.IsSuccessful );
        }

        // ===================================================================================== []
        [Test]
        public void TestConnection_Good()
        {
            var response = MipConnector.Instance.SftpHelper.TestConnection();

            Console.WriteLine( response );

            Assert.That( response.IsSuccessful );
        }

        // ===================================================================================== []
        [Test]
        public void TestConnection_Bad()
        {
            var response = MipConnector.Instance.SftpHelper.TestConnection( "wrong password" );

            Console.WriteLine( response );

            Assert.IsFalse( response.IsSuccessful );
        }
    }
}