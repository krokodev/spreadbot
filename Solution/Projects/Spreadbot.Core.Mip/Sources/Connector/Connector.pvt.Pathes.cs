namespace Spreadbot.Core.Mip
{
    public partial class Connector
    {
        // ===================================================================================== []
        private static string LocalZippedFeedFilePath(string feed, string reqId)
        {
            return string.Format(@"{0}\{1}.{2}.zip",
                Settings.ZippedFeedsPath,
                feed,
                reqId
                );
        }

        // ===================================================================================== []
        private static string RemoteFeedOutgoingZipFilePath(string feed, string reqId)
        {
            return string.Format("{0}{1}/{1}.{2}.zip",
                Settings.RemoteBasePath,
                feed,
                reqId
                );
        }

        // ===================================================================================== []
        private static string LocalFeedFolderPath(string feed)
        {
            return string.Format("{0}{1}",
                Settings.FeedsPath,
                feed
                );
        }

        // ===================================================================================== []
        private static string RemoteFeedInprocessFolderPath(string feed)
        {
            return string.Format("{0}{1}/inprocess",
                Settings.RemoteBasePath,
                feed
                );
        }
    }
}