// Spreadbot (c) 2015 Krokodev
// Spreadbot.Tests.Nunit.Amazon
// Mws_Connector_Tests.cs

using System;
using System.IO;
using NUnit.Framework;
using Spreadbot.Core.Channels.Amazon.Configuration.Settings;
using Spreadbot.Core.Channels.Amazon.Mws.Connector;
using Spreadbot.Core.Channels.Amazon.Mws.Feed;
using Spreadbot.Core.Channels.Amazon.Mws.Operations.StatusCode;
using Spreadbot.Nunit.Amazon.Base;

// Code: Mws_Connector_Tests

namespace Spreadbot.Nunit.Amazon.Tests
{
    [TestFixture]
    public class Mws_Connector_Tests : Amazon_Tests
    {
        private const string FeedFileTemplate = @"{0}Samples\SB_AMZ_002\{1}.Feed.xml";

        [SetUp]
        public static void Init() {}

        [Test]
        public void Submit_Product_Feed()
        {
            var feed = MakeMwsFeedHandler( MwsFeedType.Product );
            var response = MwsConnector.Instance.SubmitFeed( feed );

            Console.WriteLine( response );

            IgnoreMwsThrottling( response );

            Assert.AreEqual( MwsOperationStatus.SubmitFeedSuccess, response.StatusCode );
        }

        // Todo:> Submit Price, Image, Inventory

        /*
        [Test]
        public void Find_Request_Inprocess() {}

        [Test]
        public void Dont_Find_Unknown_Request_Inprocess() {}

        [Test]
        public void Get_Request_Status_Inproc() {}

        [Test]
        public void Get_Request_Status_Success() {}

        [Test]
        public void Get_Request_Status_Unknown() {}

        [Test]
        public void Response_Contains_Args_Info() {}
*/

        // ===================================================================================== []
        // Utils
        private static MwsFeedHandler MakeMwsFeedHandler( MwsFeedType mwsFeedType )
        {
            var fileNAme = string.Format( FeedFileTemplate, AmazonSettings.BasePath, mwsFeedType );
            return new MwsFeedHandler( mwsFeedType ) {
                Content = File.ReadAllText( fileNAme )
            };
        }
    }
}