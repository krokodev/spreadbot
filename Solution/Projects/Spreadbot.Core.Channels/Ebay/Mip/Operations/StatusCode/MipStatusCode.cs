// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// MipStatusCode.cs
// romak_000, 2015-03-21 2:11

namespace Spreadbot.Core.Channels.Ebay.Mip.Operations.StatusCode
{
    public enum MipStatusCode
    {
        Unknown,
        TestConnectionSuccess,
        TestConnectionFail,
        SendZippedFeedFolderSuccess,
        SendZippedFeedFolderFail,
        SendZippedFeedSuccess,
        SendZippedFeedFail,
        ZipFeedSuccess,
        ZipFeedFail,
        FindRequestSuccess,
        FindRequestFail,
        FindRemoteFileSuccess,
        FindRemoteFileFail,
        GetRequestStatusSuccess,
        GetRequestStatusFail
    }
}