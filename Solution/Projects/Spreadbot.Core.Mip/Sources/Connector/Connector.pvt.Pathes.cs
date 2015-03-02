namespace Spreadbot.Core.Mip
{
    public partial class Connector
    {
        // ===================================================================================== []
        // MakeLocalZippedFeedPath
        private static string MakeLocalZippedFeedPath(string feed, string reqId)
        {
            return string.Format(@"{0}\{1}.{2}.zip",
                Settings.ZippedFeedsPath,
                feed,
                reqId
                );
        }

        // ===================================================================================== []
        // MakeRemoteFeedInboxPath
        private static string MakeRemoteFeedOutboxPath(string feed, string reqId)
        {
            return string.Format("{0}{1}/{1}.{2}.zip",
                Settings.RemoteBasePath,
                feed,
                reqId
                );
        }

        // ===================================================================================== []
        // MakeLocalFeedPath
        private static string MakeLocalFeedPath(string feed)
        {
            return string.Format("{0}{1}",
                Settings.FeedsPath,
                feed
                );
        }
    }
}