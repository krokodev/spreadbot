namespace Spreadbot.Core.Channel.Ebay.Mip
{
    public enum MipStatusCode
    {
        Unknown,
        TestConnectionSuccess,
        TestConnectionFail,
        SendFeedFolderSuccess,
        SendFeedFolderFail,
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
