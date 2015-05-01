// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Amazon
// MwsGetFeedSubmissionCountResult.cs

using Spreadbot.Sdk.Common.Operations.ResponseResults;

namespace Spreadbot.Core.Channels.Amazon.Services.Mws.Operations.Results
{
    public class MwsGetFeedSubmissionCountResult : IResponseResult
    {
        public int FeedSubmissionCount { get; set; }
    }
}