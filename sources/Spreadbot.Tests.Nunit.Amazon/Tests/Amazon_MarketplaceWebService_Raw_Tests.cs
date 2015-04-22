// Spreadbot (c) 2015 Krokodev
// Spreadbot.Tests.Nunit.Amazon
// Amazon_MarketplaceWebService_Tests.cs

using System;
using System.Collections.Generic;
using System.IO;
using Krokodev.Common.Extensions;
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
        [Test]
        public void Product_Feed_Submitted()
        {
            SubmitFeed( "Product", "_POST_PRODUCT_DATA_" );
        }

        [Test]
        public void Image_Feed_Submitted()
        {
            SubmitFeed( "Image", "_POST_PRODUCT_IMAGE_DATA_" );
        }

        [Test]
        public void Inventory_Feed_Submitted()
        {
            SubmitFeed( "Inventory", "_POST_INVENTORY_AVAILABILITY_DATA_" );
        }

        [Test]
        public void Price_Feed_Submitted()
        {
            SubmitFeed( "Price", "_POST_PRODUCT_PRICING_DATA_" );
        }

        [Test]
        public void Recent_Feed_Submissions_Completed_Without_Errors()
        {
            var response = GetFeedSubmissionDoneList();
            if( response.GetFeedSubmissionListResult.HasNext ) {
                Assert.Inconclusive("Too many feed submissionsm need to use Next Token");
            }

            response.GetFeedSubmissionListResult.FeedSubmissionInfo.ForEach( info => {
                Console.WriteLine(info.FeedSubmissionId);
                // Code: Todo:> Check recent 10 ids with service.GetFeedSubmissionResult(..)
            } );
        }


        // ===================================================================================== []
        // Utils
        private GetFeedSubmissionListResponse GetFeedSubmissionDoneList()
        {
            var service = InitService();
            var request = new GetFeedSubmissionListRequest {
                Merchant = AmazonSettings.MerchantId,
                MaxCount = 100,
                SubmittedFromDate = DateTime.Today,
                FeedProcessingStatusList = new StatusList { Status = {"_DONE_"}}
                
            };

            var mwsResponse = service.GetFeedSubmissionList( request);
            Console.WriteLine( mwsResponse.ToXML() );
            return mwsResponse;
        }
        private static void SubmitFeed( string feedName, string feedType )
        {
            var service = InitService();

            var fileName = @"{0}Samples\SB_AMZ_002\{1}.Feed.xml".SafeFormat( AmazonSettings.BasePath, feedName );

            var request = new SubmitFeedRequest {
                Merchant = AmazonSettings.MerchantId,
                MarketplaceIdList =
                    new IdList {
                        Id = new List< string >( new[] { AmazonSettings.MarketplaceId } )
                    },
                FeedContent = File.Open( fileName, FileMode.Open, FileAccess.Read ),
                FeedType = feedType
            };
            request.ContentMD5 = MarketplaceWebServiceClient.CalculateContentMD5( request.FeedContent );
            request.FeedContent.Position = 0;
            var mwsResponse = service.SubmitFeed( request );

            Console.WriteLine( mwsResponse.ToXML() );
            Assert.That( mwsResponse.SubmitFeedResult.FeedSubmissionInfo.FeedProcessingStatus == "_SUBMITTED_" );
        }

        private static MarketplaceWebServiceClient InitService()
        {
            var config = new MarketplaceWebServiceConfig();

            config.SetUserAgentHeader( "", "", "C#" );

            config.ServiceURL = AmazonSettings.ServiceUrl;

            return new MarketplaceWebServiceClient(
                AmazonSettings.AwsAccessKeyId,
                AmazonSettings.AwsSecretAccessKey,
                config );
        }
    }
}