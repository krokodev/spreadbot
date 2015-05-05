// Spreadbot (c) 2015 Krokodev
// Spreadbot.Tests.Nunit.Amazon
// Mws_Connector_Tests.cs

using System;
using System.IO;
using Krokodev.Common.Extensions;
using NUnit.Framework;
using Spreadbot.Core.Channels.Amazon.Configuration.Settings;
using Spreadbot.Core.Channels.Amazon.Services.Mws.Connector;
using Spreadbot.Core.Channels.Amazon.Services.Mws.Feed;
using Spreadbot.Core.Channels.Amazon.Services.Mws.FeedSubmission;
using Spreadbot.Nunit.Amazon.Base;

// Code: Mws_Connector_Tests

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
        public void Just_submitted_feed_has_Inprogress_status()
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
            var submissionFeedId = "Lalala I am crazy Id!";
            var response = MwsConnector.Instance.GetFeedSubmissionProcessingStatus( submissionFeedId );

            Console.WriteLine( response );
            Assert.IsNotNull( response.Result, "response.Result" );
            var status = response.Result.FeedSubmissionProcessingStatus;

            Console.WriteLine( "Feed submission id: {0}\nProcessing status: {1}", submissionFeedId, status );
            Assert.AreEqual( MwsFeedSubmissionProcessingStatus.Unknown, status );
        }

        [Test]
        // Code: Recent_submitted_feeds_done_without_errors
        public void Recent_submitted_feeds_done_without_errors()
        {
            /*
            try {
                var response = GetFeedSubmissionDoneList();
                if( response.GetFeedSubmissionListResult.HasNext ) {
                    Assert.Inconclusive( "Too many completed feed submissions, need to use Next Token" );
                }

                var recentFeedSubmissionIds =
                    response.GetFeedSubmissionListResult.FeedSubmissionInfo.Where( info => info.IsSetFeedSubmissionId() )
                        .Reverse()
                        .ToList();
                var recentNIds =
                    recentFeedSubmissionIds.Skip( Math.Max( 0, recentFeedSubmissionIds.Count() - RecentFeedsNumber ) ).ToList();
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
                    Assert.Inconclusive( "Request is throttled" );
                }
            }*/
        }

        // ===================================================================================== []
        // Utils
        private const string FeedFileTemplate = @"{0}Samples\SB_AMZ_002\{1}.Feed.xml";

        private static MwsFeedDescriptor MakeMwsFeedHandler( MwsFeedType feedType )
        {
            var fileName = string.Format( FeedFileTemplate, AmazonSettings.BasePath, feedType );
            return new MwsFeedDescriptor( feedType ) {
                Content = File.ReadAllText( fileName )
            };
        }

        private static string SubmitFeed( MwsFeedType feedType )
        {
            var feed = MakeMwsFeedHandler( feedType );
            var response = MwsConnector.Instance.SubmitFeed( feed );

            Console.WriteLine( response );
            IgnoreMwsThrottling( response );

            Assert.That( response.IsSuccessful, feedType.ToString() );
            
            return response.Result.FeedSubmissionId;
        }
    }
}