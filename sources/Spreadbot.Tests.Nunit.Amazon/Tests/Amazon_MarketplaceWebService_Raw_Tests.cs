// Spreadbot (c) 2015 Krokodev
// Spreadbot.Tests.Nunit.Amazon
// Amazon_MarketplaceWebService_Raw_Tests.cs

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
            try {
                var response = GetFeedSubmissionDoneList();
                if( response.GetFeedSubmissionListResult.HasNext ) {
                    Assert.Inconclusive( "Too many completed feed submissions, need to use Next Token" );
                }

                const int maxCount = 5;
                var recentFeedSubmissionIds =
                    response.GetFeedSubmissionListResult.FeedSubmissionInfo.Where( info => info.IsSetFeedSubmissionId() )
                        .Reverse()
                        .ToList();
                var recentNIds =
                    recentFeedSubmissionIds.Skip( Math.Max( 0, recentFeedSubmissionIds.Count() - maxCount ) ).ToList();
                if( !recentNIds.Any() ) {
                    Assert.Inconclusive( "No completed feed submissions" );
                }

                recentNIds.ForEach( info => {
                    Console.WriteLine();
                    Console.WriteLine( info.SubmittedDate );
                    Console.WriteLine( info.FeedSubmissionId );
                    var resultResponse = GetFeedSubmissionStatus( info.FeedSubmissionId );
                    Console.WriteLine( resultResponse );
                    Assert_That_Text_Contains( resultResponse, "<MessagesWithError>0</MessagesWithError>" );
                    Assert_That_Text_Contains( resultResponse, "<MessagesWithWarning>0</MessagesWithWarning>" );
                } );
            }
            catch( MarketplaceWebServiceException e ) {
                if( e.Message.Contains( "Request is throttled" ) ) {
                    Assert.Inconclusive("Request is throttled");
                }
            }
        }

        // ===================================================================================== []
        // Utils
        private static string GetFeedSubmissionStatus( string feedSubmissionId )
        {
            var resultFileName = @"{0}Samples\SB_AMZ_002\tmp\result.{1}.xml".SafeFormat( AmazonSettings.BasePath,
                feedSubmissionId );

            using( var stream = File.Open( resultFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite ) ) {
                var service = GetService();
                var request = new GetFeedSubmissionResultRequest {
                    Merchant = AmazonSettings.MerchantId,
                    FeedSubmissionId = feedSubmissionId,
                    FeedSubmissionResult = stream
                };
                service.GetFeedSubmissionResult( request );
            }
            return File.ReadAllText( resultFileName );
        }

        private static GetFeedSubmissionListResponse GetFeedSubmissionDoneList()
        {
            var service = GetService();
            var request = new GetFeedSubmissionListRequest {
                Merchant = AmazonSettings.MerchantId,
                MaxCount = 100,
                SubmittedFromDate = DateTime.Today,
                FeedProcessingStatusList = new StatusList { Status = { "_DONE_" } }
            };
            return service.GetFeedSubmissionList( request );
        }

        private static void SubmitFeed( string feedName, string feedType )
        {
            var service = GetService();

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

        private static MarketplaceWebServiceClient GetService()
        {
            var mwsConfig = new MarketplaceWebServiceConfig();

            mwsConfig.SetUserAgentHeader( "Speadbot", "1.0", "C#" );

            mwsConfig.ServiceURL = AmazonSettings.ServiceUrl;

            return new MarketplaceWebServiceClient(
                AmazonSettings.AwsAccessKeyId,
                AmazonSettings.AwsSecretAccessKey,
                mwsConfig );
        }
    }
}