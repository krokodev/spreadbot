// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Amazon
// MwsGetFeedSubmissionOverallStatusResult.cs

using Spreadbot.Core.Channels.Amazon.Services.Mws.FeedSubmission;
using Spreadbot.Sdk.Common.Operations.ResponseResults;

namespace Spreadbot.Core.Channels.Amazon.Services.Mws.Results
{
    public class MwsGetFeedSubmissionOverallStatusResult : ResponseResult
    {
        public MwsFeedSubmissionOverallStatus Status { get; set; }
        public MwsGetFeedSubmissionCompleteStatusResult CompleteResult { get; set; }
        public MwsGetFeedSubmissionProcessingStatusResult ProcessingResult { get; set; }
    }
}