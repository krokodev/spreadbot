// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// MipOperationStatus.cs
// romak_000, 2015-03-26 19:42

namespace Spreadbot.Core.Channels.Ebay.Mip.Operations.StatusCode
{
    public enum MipOperationStatus
    {
        Unknown,
        TestConnectionSuccess,
        TestConnectionFailure,
        SendZippedFeedFolderSuccess,
        SendZippedFeedFolderFailure,
        SendZippedFeedSuccess,
        SendZippedFeedFailure,
        ZipFeedSuccess,
        ZipFeedFailure,
        FindRequestSuccess,
        FindRequestFailure,
        FindRemoteFileSuccess,
        FindRemoteFileFailure,
        GetRequestStatusSuccess,
        GetRequestStatusFailure
    }
}