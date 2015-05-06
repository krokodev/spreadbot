// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// MipFeedSubmissionResultStatus.cs

namespace Spreadbot.Core.Channels.Ebay.Services.Mip.FeedSubmission
{
    public enum MipFeedSubmissionStatus
    {
        Unknown = 0,
        Initial,
        InProgress,
        Success,
        Failure
    }
}