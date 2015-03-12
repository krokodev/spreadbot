﻿using System;
using System.Globalization;

namespace Spreadbot.Core.Channel.Ebay.Mip
{
    public partial class MipConnector
    {
        // ===================================================================================== []
        // Local
        private static string LocalZippedFeedFile(string feed, string reqId)
        {
            return string.Format(@"{0}\{1}.{2}.zip",
                MipSettings.ZippedFeedsPath,
                feed,
                reqId
                );
        }

        // --------------------------------------------------------[]
        private static string DoLocalFeedFolder(string feed)
        {
            return string.Format("{0}{1}",
                MipSettings.FeedsPath,
                feed
                );
        }

        // --------------------------------------------------------[]
        private static string LocalRequestResultsFolder()
        {
            return MipSettings.InboxPath;
        }

        // --------------------------------------------------------[]
        private static string DoLocalFeedXmlFilePath(MipFeed mipFeed)
        {
            return string.Format(@"{0}\{1}.{2}.xml",
                DoLocalFeedFolder(mipFeed.Name),
                mipFeed.Name,
                (Guid)mipFeed.Id
                );
        }


        // ===================================================================================== []
        // Remote
        private static string RemoteFeedOutgoingZipFilePath(string feed, string reqId)
        {
            return string.Format("{0}{1}/{1}.{2}.zip",
                MipSettings.RemoteBasePath,
                feed,
                reqId
                );
        }

        // --------------------------------------------------------[]
        private static string RemoteFeedInprocessFolderPath(string feed)
        {
            return string.Format("{0}{1}/inprocess",
                MipSettings.RemoteBasePath,
                feed
                );
        }

        // --------------------------------------------------------[]
        private static string[] RemoteFeedOutputFolderPathes(string feed)
        {
            return new []
            {
                RemoteFeedOutputFolderPath(feed, 0),
                RemoteFeedOutputFolderPath(feed, -1),
                RemoteFeedOutputFolderPath(feed, 1),
                RemoteFeedOutputFolderPath(feed, -2)
            };
        }


        // ===================================================================================== []
        // Remote Data Based Names
        private static string RemoteFeedOutputFolderPath(string feed, int dayShift)
        {
            return string.Format("{0}{1}/output/{2}",
                MipSettings.RemoteBasePath,
                feed,
                DataBasedFolderName(dayShift)
                );
        }

        // ===================================================================================== []
        private static string DataBasedFolderName(int dayShift)
        {
            var hourOffset = MipSettings.OutputFolderNameUtcHourOffset;
            var utcNow = DateTime.UtcNow;
            var mipNow = utcNow.AddHours(hourOffset + 24*dayShift);

            return mipNow.Date.ToString("MMM-dd-yyy", CultureInfo.CreateSpecificCulture("en-US"));
        }
    }
}