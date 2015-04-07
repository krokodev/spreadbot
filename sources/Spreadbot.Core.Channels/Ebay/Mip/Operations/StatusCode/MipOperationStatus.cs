// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// MipOperationStatus.cs
// Roman, 2015-04-07 2:58 PM

namespace Spreadbot.Core.Channels.Ebay.Mip.Operations.StatusCode
{
    public enum MipOperationStatus
    {
        Unknown,
        TestConnectionSuccess,
        TestConnectionFailure,
        SendFeedSuccess,
        SendFeedFailure,
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