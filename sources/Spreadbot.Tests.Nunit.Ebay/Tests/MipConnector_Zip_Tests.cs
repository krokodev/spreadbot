﻿// Spreadbot (c) 2015 Krokodev
// Spreadbot.Tests.Nunit.Ebay
// MipConnector_Zip_Tests.cs

using System;
using System.IO;
using NUnit.Framework;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Connector;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Services.Mip.FeedSubmission;
using Spreadbot.Nunit.Ebay.Base;
using Spreadbot.Nunit.Ebay.Utils;

namespace Spreadbot.Nunit.Ebay.Tests
{
    [TestFixture]
    public class MipConnector_Zip_Tests : Ebay_Tests
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
            var feed = new MipFeedDescriptor( MipFeedType.Product );
            var reqId = MipFeedSubmissionDescriptor.GenerateZeroId();

            var response = MipConnector.Instance.ZipHelper.ZipFeed( feed, reqId );
            Console.WriteLine( response );

            Assert.That( response.IsSuccessful );
            Assert.IsTrue( File.Exists( response.Result.ZipFileName ) );
        }
    }
}