// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// MipSubmitFeedResult.cs

using Spreadbot.Sdk.Common.Operations.ResponseResults;

namespace Spreadbot.Core.Channels.Ebay.Services.Mip.Results
{
    public class MipSubmitFeedResult : ResponseResult
    {
        public string FeedSubmissionId { get; set; }
    }
}