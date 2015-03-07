namespace Spreadbot.Core.Channel.Ebay.Mip
{
    public enum StatusCode
    {
        Unknown,
        TestConnectionSuccess,
        TestConnectionFail,
        SendFeedSuccess,
        SendFeedFail,
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
