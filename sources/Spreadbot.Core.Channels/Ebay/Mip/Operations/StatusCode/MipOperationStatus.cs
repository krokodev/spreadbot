// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// MipOperationStatus.cs
// Roman, 2015-03-31 1:27 PM

namespace Spreadbot.Core.Channels.Ebay.Mip.Operations.StatusCode
{
    public enum MipOperationStatus
    {
        Unknown,
        TestConnectionSuccess,
        TestConnectionFailure,
        SendZippedFeedFolderSuccess,
        SendZippedFeedFolderFailure,
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