// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Amazon
// MwsSubmitFeedResult.cs

using Spreadbot.Sdk.Common.Operations.ResponseResults;

namespace Spreadbot.Core.Channels.Amazon.Services.Mws.Results
{
    public class MwsSubmitFeedResult : ResponseResult
    {
        public string FeedSubmissionId { get; set; }
    }
}