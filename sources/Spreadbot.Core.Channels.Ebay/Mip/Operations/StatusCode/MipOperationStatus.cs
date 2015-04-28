// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// MipOperationStatus.cs

namespace Spreadbot.Core.Channels.Ebay.Mip.Operations.StatusCode
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
        FindRequestSuccess,
        FindRequestFailure,
        FindRemoteFileSuccess,
        FindRemoteFileFailure,
        GetRequestStatusSuccess,
        GetRequestStatusFailure,
        SftpSendFilesSuccess,
        SftpSendFilesFailure
    }
}