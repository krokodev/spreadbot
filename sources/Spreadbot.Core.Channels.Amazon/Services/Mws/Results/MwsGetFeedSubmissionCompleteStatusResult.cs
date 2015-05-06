// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Amazon
// MwsGetFeedSubmissionCompleteStatusResult.cs

using Spreadbot.Core.Channels.Amazon.Services.Mws.FeedSubmission;
using Spreadbot.Sdk.Common.Operations.ResponseResults;

namespace Spreadbot.Core.Channels.Amazon.Services.Mws.Results
{
    public class MwsGetFeedSubmissionCompleteStatusResult : ResponseResult
    {
        public MwsFeedSubmissionCompleteStatus FeedSubmissionCompleteStatus { get; set; }
        public int? TotalProcessedCount { get; set; }
        public int? SuccessfulCount { get; set; }
        public int? WithErrorCount { get; set; }
        public int? WithWarningCount { get; set; }
        public string Content { get; set; }
    }
}