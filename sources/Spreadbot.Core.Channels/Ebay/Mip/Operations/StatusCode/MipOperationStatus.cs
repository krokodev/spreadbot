// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// MipOperationStatus.cs
// Roman, 2015-04-03 1:45 PM

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