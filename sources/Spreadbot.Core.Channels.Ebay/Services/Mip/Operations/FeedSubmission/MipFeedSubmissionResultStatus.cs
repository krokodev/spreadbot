// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// MipSubmissionStatus.cs

namespace Spreadbot.Core.Channels.Ebay.Services.Mip.Operations.FeedSubmission
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