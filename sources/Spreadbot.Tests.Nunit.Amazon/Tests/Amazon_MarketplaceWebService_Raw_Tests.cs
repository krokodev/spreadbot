// Spreadbot (c) 2015 Krokodev
// Spreadbot.Tests.Nunit.Amazon
// Amazon_MarketplaceWebService_Raw_Tests.cs

using System;
using System.Collections.Generic;
using System.IO;
using MarketplaceWebService;
using MarketplaceWebService.Model;
using NUnit.Framework;
using Spreadbot.Core.Channels.Amazon.Configuration.Settings;
using Spreadbot.Nunit.Amazon.Base;

namespace Spreadbot.Nunit.Amazon.Tests
{
    [TestFixture]
    public class Amazon_MarketplaceWebService_Raw_Tests : Amazom_Tests
    {
        private const string FeedType = "_POST_PRODUCT_DATA_";

        [Test]
        public void Send_Product_Feed()
        {
            var config = new MarketplaceWebServiceConfig();
            var fileName = AmazonSettings.BasePath + @"Samples\Product.Test.Feed.xml";

            config.SetUserAgentHeader( "", "", "C#" );

            config.ServiceURL = AmazonSettings.ServiceUrl;

            var service = new MarketplaceWebServiceClient(
                AmazonSettings.AwsAccessKeyId,
                AmazonSettings.AwsSecretAccessKey,
                config );

            var request = new SubmitFeedRequest {
                Merchant = AmazonSettings.MerchantId,
                MWSAuthToken = null,
                MarketplaceIdList =
                    new IdList {
                        Id = new List< string >( new[] { AmazonSettings.MarketplaceId } )
                    },

                FeedContent = File.Open( fileName, FileMode.Open, FileAccess.Read ),
                FeedType = FeedType
            };
            request.ContentMD5 = MarketplaceWebServiceClient.CalculateContentMD5( request.FeedContent );
            request.FeedContent.Position = 0;

            var response = service.SubmitFeed( request );
            Console.WriteLine( response.ToXML() );
            Assert_That_Text_Contains( response.ToXML(), "<FeedProcessingStatus>_SUBMITTED_</FeedProcessingStatus>" );
        }
    }
}