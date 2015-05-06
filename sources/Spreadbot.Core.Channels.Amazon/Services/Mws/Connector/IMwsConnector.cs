// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Amazon
// IMwsConnector.cs

using Spreadbot.Core.Channels.Amazon.Services.Mws.Feed;
using Spreadbot.Core.Channels.Amazon.Services.Mws.FeedSubmission;
using Spreadbot.Core.Channels.Amazon.Services.Mws.Results;
using Spreadbot.Sdk.Common.Operations.Responses;

namespace Spreadbot.Core.Channels.Amazon.Services.Mws.Connector
{
    public interface IMwsConnector
    {
        Response< MwsSubmitFeedResult > SubmitFeed( MwsFeedDescriptor feedDescriptor );
        Response< MwsGetFeedSubmissionListResult > GetFeedSubmissionList( MwsSubmittedFeedsFilter filter );
        Response< MwsGetFeedSubmissionCountResult > GetFeedSubmissionCount( MwsSubmittedFeedsFilter filter );

        Response< MwsGetFeedSubmissionProcessingStatusResult > GetFeedSubmissionProcessingStatus(
            string feedSubmissionId );

        Response< MwsGetFeedSubmissionCompleteStatusResult > GetFeedSubmissionCompleteStatus( string feedSubmissionId );
        Response< MwsGetFeedSubmissionOverallStatusResult > GetFeedSubmissionOverallStatus( string feedSubmissionId );
    }
}