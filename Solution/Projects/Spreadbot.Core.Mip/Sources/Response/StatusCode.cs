namespace Spreadbot.Core.Mip
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
        FindRequestFail
    }
}
