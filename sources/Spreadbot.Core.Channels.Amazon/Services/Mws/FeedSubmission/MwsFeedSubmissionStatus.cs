// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Amazon
// MwsFeedSubmissionResultStatus.cs

namespace Spreadbot.Core.Channels.Amazon.Services.Mws.FeedSubmission
{
    public enum MwsFeedSubmissionStatus
    {
        Unknown = 0,
        Initial,
        InProgress,
        Success,
        Failure
    }
}