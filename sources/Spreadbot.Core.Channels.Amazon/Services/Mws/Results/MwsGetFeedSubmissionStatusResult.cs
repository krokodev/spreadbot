// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Amazon
// MwsGetFeedSubmissionStatusResult.cs

using Spreadbot.Core.Channels.Amazon.Services.Mws.FeedSubmission;
using Spreadbot.Sdk.Common.Operations.ResponseResults;

namespace Spreadbot.Core.Channels.Amazon.Services.Mws.Results
{
    public class MwsGetFeedSubmissionStatusResult : ResponseResult
    {
        public MwsFeedSubmissionStatus FeedSubmissionStatus { get; set; }
        public string Content { get; set; }
    }
}