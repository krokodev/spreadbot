// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Amazon
// MwsSubmissionStatus.cs

namespace Spreadbot.Core.Channels.Amazon.Services.Mws.Operations.Submission
{
    public enum MwsSubmissionStatus
    {
        Unknown = 0,
        Initial,
        Inprocess,
        Success,
        Failure
    }
}