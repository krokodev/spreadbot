// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Amazon
// MwsSubmittedFeedsFilter.cs

using System;
using System.Collections.Generic;
using Spreadbot.Core.Channels.Amazon.Services.Mws.Feed;

namespace Spreadbot.Core.Channels.Amazon.Services.Mws.FeedSubmission
{
    public class MwsSubmittedFeedsFilter
    {
        public MwsSubmittedFeedsFilter()
        {
            ProcessingStatusList = new List< MwsFeedSubmissionProcessingStatus >();
            FromDate = DateTime.Now;
            ToDate = DateTime.Now;
            IdList = new List< string >();
            FeedTypeList = new List< MwsFeedType >();
        }

        public List< MwsFeedSubmissionProcessingStatus > ProcessingStatusList { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public List< string > IdList { get; set; }
        public List< MwsFeedType > FeedTypeList { get; set; }

        public const int MaxFeedSubmissionAgeInDays = 90;

        public static MwsSubmittedFeedsFilter All(
            MwsFeedType? feedType = null,
            MwsFeedSubmissionProcessingStatus? status = null,
            int daysFromToday = MaxFeedSubmissionAgeInDays )
        {
            return new MwsSubmittedFeedsFilter {
                FeedTypeList =
                    feedType != null
                        ? new List< MwsFeedType > { ( MwsFeedType ) feedType }
                        : new List< MwsFeedType >(),
                ProcessingStatusList =
                    status != null
                        ? new List< MwsFeedSubmissionProcessingStatus > { ( MwsFeedSubmissionProcessingStatus ) status }
                        : new List< MwsFeedSubmissionProcessingStatus >(),
                ToDate = DateTime.Now,
                FromDate = DateTime.Now.Subtract( TimeSpan.FromDays( daysFromToday ) )
            };
        }

        public static MwsSubmittedFeedsFilter LastDays( int daysNum )
        {
            return All( null, null, daysNum );
        }

        public static MwsSubmittedFeedsFilter WithId( string feedSubmissionId )
        {
            var filter = All();
            filter.IdList = new List< string > { feedSubmissionId };
            return filter;
        }

        public static MwsSubmittedFeedsFilter DoneInLastDays( int daysNum )
        {
            return All( null, MwsFeedSubmissionProcessingStatus.Complete, daysNum );
        }
    }
}