// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// MipFeedSubmissionResultStatus.cs

namespace Spreadbot.Core.Channels.Ebay.Services.Mip.FeedSubmission
{
    public enum MipFeedSubmissionResultStatus
    {
        Unknown = 0,
        Initial,
        Inprocess,
        Success,
        Failure
    }
}