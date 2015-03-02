using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

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

        // ===================================================================================== []
        private static IList<string> RemoteFeedOutputFolderPathes(string feed)
        {
            return new List<string>
            {
                RemoteFeedOutputFolderPath(feed, 0),
                RemoteFeedOutputFolderPath(feed, -1),
                RemoteFeedOutputFolderPath(feed, 1),
                RemoteFeedOutputFolderPath(feed, -2)
            };
        }

        // ===================================================================================== []
        private static string RemoteFeedOutputFolderPath(string feed, int dayShift)
        {
            return string.Format("{0}{1}/output/{2}",
                Settings.RemoteBasePath,
                feed,
                DataBasedFolderName(dayShift)
                );
        }

        // ===================================================================================== []
        private static string DataBasedFolderName(int dayShift)
        {
            int hourOffset = Settings.OutputFolderNameUtcHourOffset
                -7;

            var utcNow = DateTime.UtcNow;
            var mipNow = utcNow.AddHours(hourOffset + 24 * dayShift);

            return mipNow.Date.ToString("MMM-dd-yyy", CultureInfo.CreateSpecificCulture("en-US"));
        }
    }
}