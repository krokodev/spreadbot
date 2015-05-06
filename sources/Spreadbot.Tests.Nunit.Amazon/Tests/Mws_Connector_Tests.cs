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

            IgnoreMwsThrottling( response );

            Assert.IsNotNull( response.Result, "Result" );
            Assert.IsNotNull( response.Result.FeedSubmissionDescriptors, "Result.FeedSubmissionIds" );
            Assert.GreaterOrEqual( response.Result.FeedSubmissionDescriptors.Count, 0 );

            Console.WriteLine( response.Result.FeedSubmissionDescriptors.FoldToStringBy( d => d.FeedSubmissionId, "\n" ) );
        }

        [Test]
        public void Get_Submitted_Feeds_List_Count()
        {
            var response = MwsConnector.Instance.GetFeedSubmissionCount( MwsSubmittedFeedsFilter.All() );

            IgnoreMwsThrottling( response );

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

            IgnoreMwsThrottling( responseCount );
            IgnoreMwsThrottling( responseInfo );

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
            const int recentNumber = 7;
            var listResponse = MwsConnector.Instance.GetFeedSubmissionList( MwsSubmittedFeedsFilter.DoneInLastDays( 10 ) );
            listResponse.Check();

            var recentSubmissionIds = GetRecentFeedSubmissionIds( listResponse, recentNumber );

            recentSubmissionIds.ForEach( id => {
                Console.WriteLine();
                Console.WriteLine( id );

                var response = MwsConnector.Instance.GetFeedSubmissionCompleteStatus( id );
                response.Check();
                Console.WriteLine( response );

                Assert.AreEqual( MwsFeedSubmissionCompleteStatus.Success, response.Result.FeedSubmissionCompleteStatus );
                Assert.AreEqual( 0, response.Result.WithErrorCount );
                Assert.AreEqual( 0, response.Result.WithWarningCount );
                Assert.AreEqual( response.Result.TotalProcessedCount, response.Result.SuccessfulCount );

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
            response.Check();

            Console.WriteLine( feedSubmissionId );
            Console.WriteLine( response );
            Assert.AreEqual( MwsFeedSubmissionOverallStatus.Success, response.Result.FeedSubmissionOverallStatus );
            Assert_That_Text_Contains( response, "FeedSubmissionProcessingStatus" );
            Assert_That_Text_Contains( response, "FeedSubmissionCompleteStatus" );
        }

        [Test]
        public void Error_code_and_description_available_on_failed_submission()
        {
            // send wrong image request (you can use bad xml) 
            // find failed response
            // read error code
            // Ignore images in 'recent must be ok test'
            // see https://sellercentral.amazon.com/forums/thread.jspa?threadID=12023
        }

        [Test]
        public void Obtain_submitted_products_id_list()
        {
            // Use Product API
        }


        #region Utils

        private const string FeedFileTemplate = @"{0}Samples\SB_AMZ_002\{1}.Feed.xml";

        private static MwsFeedDescriptor MakeMwsFeedHandler( MwsFeedType feedType )
        {
            var fileName = string.Format( FeedFileTemplate, AmazonSettings.BasePath, feedType );
            return new MwsFeedDescriptor( feedType ) {
                Content = File.ReadAllText( fileName )
            };
        }

        private static string SubmitFeed( MwsFeedType feedType, bool mute = false )
        {
            var feed = MakeMwsFeedHandler( feedType );
            var response = MwsConnector.Instance.SubmitFeed( feed );

            if( !mute ) {
                Console.WriteLine( response );
            }
            IgnoreMwsThrottling( response );

            Assert.That( response.IsSuccessful, feedType.ToString() );

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
            var response = MwsConnector.Instance.GetFeedSubmissionList( filter ).Check();
            return response.Result.FeedSubmissionDescriptors.Select( d => d.FeedSubmissionId ).FirstOrDefault();
        }

        #endregion
    }
}