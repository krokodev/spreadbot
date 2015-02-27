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
        private static string MakeLocalZippedFeedPath(string feed, string reqId)
        {
            return string.Format("{0}{1}.{2}.zip",
                Settings.ZippedFeedsPath,
                feed,
                reqId
                );
        }

        private static string MakeRemoteFeedInboxPath(string feed, string reqId)
        {
            return string.Format("{0}{1}/{1}.{2}.zip",
                Settings.RemoteBasePath,
                feed,
                reqId
                );
        }
    }
}