// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Amazon
// MwsSubmissionStatus.cs

namespace Spreadbot.Core.Channels.Amazon.Services.Mws.Operations.FeedSubmission
{
    public enum MwsFeedSubmissionResultStatus
    {
        Unknown = 0,
        Initial,
        InProgress,
        Success,
        Failure
    }
}