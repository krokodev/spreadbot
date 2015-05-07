// Spreadbot (c) 2015 Krokodev
// Spreadbot.Tests.Nunit.Amazon
// Mws_Connector_Tests.cs

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Krokodev.Common.Extensions;
using MoreLinq;
using NUnit.Framework;
using Spreadbot.Core.Channels.Amazon.Configuration.Settings;
using Spreadbot.Core.Channels.Amazon.Services.Mws.Connector;
using Spreadbot.Core.Channels.Amazon.Services.Mws.Feed;
using Spreadbot.Core.Channels.Amazon.Services.Mws.FeedSubmission;
using Spreadbot.Core.Channels.Amazon.Services.Mws.Results;
using Spreadbot.Nunit.Amazon.Base;
using Spreadbot.Sdk.Common.Operations.Responses;

// Here: Mws_Connector_Tests

namespace Spreadbot.Nunit.Amazon.Tests
{
    [TestFixture]
    public class Mws_Connector_Tests : Amazon_Tests
    {
        [SetUp]
        public static void Init() {}

        [Test]
        public void Submit_Product_Feed()
        {
            SubmitFeed( MwsFeedType.Product );
        }

        [Test]
        public void Submit_Inventory_Feed()
        {
            SubmitFeed( MwsFeedType.Inventory );
        }

        [Test]
        public void Submit_Price_Feed()
        {
            SubmitFeed( MwsFeedType.Price );
        }

        [Test]
        public void Submit_Image_Feed()
        {
            SubmitFeed( MwsFeedType.Image );
        }

        [Test]
        public void Get_Submitted_Feeds_List()
        {
            var filter = new MwsSubmittedFeedsFilter();
            var response = MwsConnector.Instance.GetFeedSubmissionList( filter );

            Ignore_Mws_Throttling( response );

            Assert.IsNotNull( response.Result, "Result" );
            Assert.IsNotNull( response.Result.FeedSubmissionDescriptors, "Result.FeedSubmissionIds" );
            Assert.GreaterOrEqual( response.Result.FeedSubmissionDescriptors.Count, 0 );

            Console.WriteLine( response.Result.FeedSubmissionDescriptors.FoldToStringBy( d => d.FeedSubmissionId, "\n" ) );
        }

        [Test]
        public void Get_Submitted_Feeds_List_Count()
        {
            var response = MwsConnector.Instance.GetFeedSubmissionCount( MwsSubmittedFeedsFilter.All() );

            Ignore_Mws_Throttling( response );

            Assert.IsNotNull( response.Result, "Result" );
            Assert.GreaterOrEqual( response.Result.FeedSubmissionCount, 0 );

            Console.WriteLine( "Count: {0}", response.Result.FeedSubmissionCount );
        }

        [Test]
        public void Get_Submitted_Feeds_List_Count_Equal_GetList_Count()
        {
            var filter = MwsSubmittedFeedsFilter.LastDays( 10 );
            var responseCount = MwsConnector.Instance.GetFeedSubmissionCount( filter );
            var responseInfo = MwsConnector.Instance.GetFeedSubmissionList( filter );

            Ignore_Mws_Throttling( responseCount );
            Ignore_Mws_Throttling( responseInfo );

            Assert.IsNotNull( responseCount.Result, "responseCount.Result" );
            Assert.IsNotNull( responseInfo.Result, "responseInfo.Result" );

            var count1 = responseCount.Result.FeedSubmissionCount;
            var count2 = responseInfo.Result.FeedSubmissionDescriptors.Count;
            Console.WriteLine( "Count1: {0}\nCount2: {1}", count1, count2 );

            Assert.AreEqual( count1, count2, "Counts form different responces" );
        }

        [Test]
        public void Just_submitted_feed_has_InProgress_processing_status()
        {
            var submissionFeedId = SubmitFeed( MwsFeedType.Product );
            var response = MwsConnector.Instance.GetFeedSubmissionProcessingStatus( submissionFeedId );

            Assert.IsNotNull( response.Result, "response.Result" );
            var status = response.Result.FeedSubmissionProcessingStatus;

            Console.WriteLine( "Feed submission id: {0}\nProcessing status: {1}", submissionFeedId, status );
            Assert.AreEqual( MwsFeedSubmissionProcessingStatus.InProgress, status );
        }

        [Test]
        public void Incorrect_SubmissionFeedId_involves_Unknown_processing_status()
        {
            const string wrongId = "Lalala I am crazy Id!";
            var response = MwsConnector.Instance.GetFeedSubmissionProcessingStatus( wrongId );

            Console.WriteLine( response );
            Assert.IsNotNull( response.Result, "response.Result" );
            var status = response.Result.FeedSubmissionProcessingStatus;

            Console.WriteLine( "Feed submission id: {0}\nProcessing status: {1}", wrongId, status );
            Assert.AreEqual( MwsFeedSubmissionProcessingStatus.Unknown, status );
        }

        [Test]
        public void Recent_submitted_feeds_done_without_errors()
        {
            const int recentNumber = 5;
            var listResponse = MwsConnector.Instance.GetFeedSubmissionList( MwsSubmittedFeedsFilter.DoneInLastDays( 10 ) );
            listResponse.Check();

            var recentSubmissionIds = GetRecentFeedSubmissionIds( listResponse, recentNumber );

            recentSubmissionIds.ForEach( id => {
                Console.WriteLine();
                Console.WriteLine( id );

                var response = MwsConnector.Instance.GetFeedSubmissionCompleteStatus( id );
                Console.WriteLine( response );
                Ignore_Mws_Throttling( response );
                Ignore_Some_Errors_Advisely_Generated_by_Tests( response );
                response.Check();

                Assert.AreEqual( MwsFeedSubmissionCompleteStatus.Success, response.Result.FeedSubmissionCompleteStatus );
                Assert.AreEqual( id, response.Result.TransactionId );
                Assert.AreEqual( 0, response.Result.WithErrorCount );
                Assert.AreEqual( 0, response.Result.WithWarningCount );
                Assert.AreEqual( response.Result.ProcessedCount, response.Result.SuccessfulCount );

                Assert_That_Text_Contains( response.Result.Content, "<MessagesWithError>0</MessagesWithError>" );
                Assert_That_Text_Contains( response.Result.Content, "<MessagesWithWarning>0</MessagesWithWarning>" );
            } );
        }

        [Test]
        public void Just_submitted_feed_has_InProgress_overall_status()
        {
            var feedSubmissionId = SubmitFeed( MwsFeedType.Product, mute : true );
            var response = MwsConnector.Instance.GetFeedSubmissionOverallStatus( feedSubmissionId );
            response.Check();

            Console.WriteLine( response );
            Assert.AreEqual( MwsFeedSubmissionOverallStatus.InProgress, response.Result.FeedSubmissionOverallStatus );
            Assert_That_Text_Contains( response, "FeedSubmissionProcessingStatus" );
        }

        [Test]
        public void Long_submitted_product_feed_has_Successful_overall_status()
        {
            var feedSubmissionId = FindProductFeedSubmissionWithDoneStatus();
            var response = MwsConnector.Instance.GetFeedSubmissionOverallStatus( feedSubmissionId );

            Console.WriteLine( feedSubmissionId );
            Console.WriteLine( response );

            Ignore_Mws_Throttling( response );
            Ignore_Some_Errors_Advisely_Generated_by_Tests( response );

            response.Check();

            Assert.AreEqual( MwsFeedSubmissionOverallStatus.Success, response.Result.FeedSubmissionOverallStatus );
            Assert_That_Text_Contains( response, "FeedSubmissionProcessingStatus" );
            Assert_That_Text_Contains( response, "FeedSubmissionCompleteStatus" );
        }

        [Test]
        public void Just_submitted_price_feed_found_in_total_id_list()
        {
            var feedSubmissionId = SubmitFeed( MwsFeedType.Price, mute : true );
            Console.WriteLine( "New id:\n{0}\n", feedSubmissionId );

            var filter = MwsSubmittedFeedsFilter.All( MwsFeedType.Price );
            var listResponse = MwsConnector.Instance.GetFeedSubmissionList( filter );
            listResponse.Check();

            const int recentNumber = 20;
            var recentSubmissionIds = GetRecentFeedSubmissionIds( listResponse, recentNumber ).Reverse().ToList();
            Console.WriteLine( "\nRecent ids:\n" );
            recentSubmissionIds.ForEach( Console.WriteLine );

            Assert.That( recentSubmissionIds.Contains( feedSubmissionId ), "List contains new id" );
        }

        [Test]
        public void Error_code_and_description_available_on_failed_submission()
        {
            TrySubmitProductFeedAsImageFeed();

            const int recentNumber = 5;
            var filter = MwsSubmittedFeedsFilter.All( MwsFeedType.Image, MwsFeedSubmissionProcessingStatus.Complete );
            var listResponse = MwsConnector.Instance.GetFeedSubmissionList( filter );
            listResponse.Check();

            var recentSubmissionIds = GetRecentFeedSubmissionIds( listResponse, recentNumber ).Reverse().ToList();

            var statusResponse = FindFirstFailedFeedSubmission( recentSubmissionIds );
            Assert.NotNull( statusResponse, "statusResponse" );
            statusResponse.Check();
            Console.WriteLine( statusResponse );

            Assert_That_Text_Contains( statusResponse.Result.Content, "<ResultCode>Error</ResultCode>" );
            Assert_That_Text_Contains( statusResponse.Result.Content, "<ResultMessageCode>5000</ResultMessageCode>" );
            Assert_That_Text_Contains( statusResponse.Result.Content, "Please specify the correct feed" );

            // todo:> parse error code and descrition
        }

        [Test]
        public void Submitted_products_id_can_be_achieved()
        {
            // todo:> Use Product API
        }


        #region Utils

        private static Response< MwsGetFeedSubmissionCompleteStatusResult > FindFirstFailedFeedSubmission(
            IEnumerable< string > ids )
        {
            foreach( var id in ids ) {
                var statusResponse = MwsConnector.Instance.GetFeedSubmissionCompleteStatus( id );
                Ignore_Mws_Throttling( statusResponse );

                var status = statusResponse.Result.FeedSubmissionCompleteStatus;
                Console.WriteLine( "{0} {1}", id, status );
                if( status == MwsFeedSubmissionCompleteStatus.Failure ) {
                    return statusResponse;
                }
            }
            return null;
        }

        private const string FeedFileTemplate = @"{0}Samples\SB_AMZ_002\{1}.Feed.xml";

        private static MwsFeedDescriptor MakeMwsFeedHandler( MwsFeedType feedType )
        {
            var fileName = string.Format( FeedFileTemplate, AmazonSettings.BasePath, feedType );
            return new MwsFeedDescriptor( feedType ) {
                Content = File.ReadAllText( fileName )
            };
        }

        private static void TrySubmitProductFeedAsImageFeed()
        {
            try {
                var fileName = string.Format( FeedFileTemplate, AmazonSettings.BasePath, MwsFeedType.Product );
                var feed = new MwsFeedDescriptor( MwsFeedType.Image ) {
                    Content = File.ReadAllText( fileName )
                };
                var response = MwsConnector.Instance.SubmitFeed( feed );
                Ignore_Mws_Throttling( response );
                Console.WriteLine( response );
                Assert.That( response.IsSuccessful, "SubmitProductFeedAsImageFeed" );
            }
            catch {
                Console.WriteLine( "TrySubmitProductFeedAsImageFeed: Failed" );
            }
        }

        private static string SubmitFeed( MwsFeedType feedType, bool mute = false )
        {
            var feed = MakeMwsFeedHandler( feedType );
            var response = MwsConnector.Instance.SubmitFeed( feed );

            if( !mute ) {
                Console.WriteLine( response );
            }
            Ignore_Mws_Throttling( response );
            response.Check();
            return response.Result.FeedSubmissionId;
        }

        private static IEnumerable< string > GetRecentFeedSubmissionIds(
            Response< MwsGetFeedSubmissionListResult > response,
            int num )
        {
            var recentFeedSubmissionIds = response.Result.FeedSubmissionDescriptors
                .Select( d => d.FeedSubmissionId )
                .Reverse()
                .ToList();

            var recentIds = recentFeedSubmissionIds
                .Skip( Math.Max( 0, recentFeedSubmissionIds.Count() - num ) )
                .ToList();

            if( !recentIds.Any() ) {
                Assert.Inconclusive( "No completed feed submissions" );
            }
            return recentIds;
        }

        private static string FindProductFeedSubmissionWithDoneStatus()
        {
            var filter = MwsSubmittedFeedsFilter.All( MwsFeedType.Product,
                MwsFeedSubmissionProcessingStatus.Complete,
                10 );
            var response = MwsConnector.Instance.GetFeedSubmissionList( filter );
            Ignore_Mws_Throttling( response );
            response.Check();

            return response.Result.FeedSubmissionDescriptors.Select( d => d.FeedSubmissionId ).FirstOrDefault();
        }

        #endregion
    }
}