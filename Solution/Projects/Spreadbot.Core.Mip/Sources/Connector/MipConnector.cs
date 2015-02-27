// ReSharper disable RedundantUsingDirective
using System;
using Crocodev.Common.Identifier;
using WinSCP;

namespace Spreadbot.Core.Mip
{
    public partial class MipConnector
    {
        // >> Now: MipConnector



        // ===================================================================================== []
        // Paths
        private static string MakeLocalZippedFeedPath(MipFeed feed, MipRequest.Identifier reqId)
        {
            return string.Format("{0}{1}.{2}.zip",
                MipSettings.ZippedFeedsPath,
                feed.Name,
                reqId
                );
        }

        private static string MakeRemoteFeedInboxPath(MipFeed feed, MipRequest.Identifier reqId)
        {
            return string.Format("{0}{1}/{1}.{2}.zip",
                MipSettings.RemoteBasePath,
                feed.Name,
                reqId
                );
        }
    }
}