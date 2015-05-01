// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// MipGetSubmissionStatusResult.cs

using Spreadbot.Core.Channels.Ebay.Services.Mip.Operations.FeedSubmission;
using Spreadbot.Sdk.Common.Operations.ResponseResults;

namespace Spreadbot.Core.Channels.Ebay.Services.Mip.Operations.Results
{
    public class MipGetSubmissionStatusResult : ResponseResult
    {
        public MipFeedSubmissionResultStatus MipFeedSubmissionResultStatusCode { get; set; }
        public string MipItemId { get; set; }
        public string Details { get; set; }
    }
}