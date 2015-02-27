// ReSharper disable RedundantUsingDirective
using System;
using Crocodev.Common.Identifier;
using WinSCP;

namespace Spreadbot.Core.Mip
{
    public partial class Connector
    {
        // ===================================================================================== []
        // Paths
        private static string MakeLocalZippedFeedPath(Feed feed, Request.Identifier reqId)
        {
            return string.Format("{0}{1}.{2}.zip",
                Settings.ZippedFeedsPath,
                feed.Name,
                reqId
                );
        }

        private static string MakeRemoteFeedInboxPath(Feed feed, Request.Identifier reqId)
        {
            return string.Format("{0}{1}/{1}.{2}.zip",
                Settings.RemoteBasePath,
                feed.Name,
                reqId
                );
        }
    }
}