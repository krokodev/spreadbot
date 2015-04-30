// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// MipOperationStatus.cs

namespace Spreadbot.Core.Channels.Ebay.Services.Mip.Operations.StatusCode
{
    public enum MipOperationStatus
    {
        Unknown,
        TestConnectionSuccess,
        TestConnectionFailure,
        SubmitFeedSuccess,
        SubmitFeedFailure,
        ZipFeedSuccess,
        ZipFeedFailure,
        FindSubmissionSuccess,
        FindSubmissionFailure,
        FindRemoteFileSuccess,
        FindRemoteFileFailure,
        GetSubmissionStatusSuccess,
        GetSubmissionStatusFailure,
        SftpSendFilesSuccess,
        SftpSendFilesFailure
    }
}