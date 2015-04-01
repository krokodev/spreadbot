// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// MipConnector.pvt.Pathes.cs
// Roman, 2015-03-31 1:26 PM

using System;
using System.Globalization;
using Spreadbot.Core.Channels.Ebay.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Mip.Settings;

namespace Spreadbot.Core.Channels.Ebay.Mip.Connector
{
    public partial class MipConnector
    {
        // ===================================================================================== []
        // Local
        public static string LocalZippedFeedFile( string feed, string reqId )
        {
            return string.Format(
                @"{0}\{1}.{2}.zip",
                MipSettings.ZippedFeedsPath,
                feed,
                reqId
                );
        }

        // --------------------------------------------------------[]
        private static string DoLocalFeedFolder( string feed )
        {
            return string.Format(
                "{0}{1}",
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
        private static string DoLocalFeedXmlFilePath( MipFeedHandler mipFeedHandler )
        {
            return string.Format(
                @"{0}\{1}.{2}.xml",
                DoLocalFeedFolder( mipFeedHandler.GetName() ),
                mipFeedHandler.GetName(),
                mipFeedHandler.Id
                );
        }

        // ===================================================================================== []
        // Remote
        public static string RemoteFeedOutgoingZipFilePath( string feed, string reqId )
        {
            return string.Format(
                "{0}{1}/{1}.{2}.zip",
                MipSettings.RemoteBasePath,
                feed,
                reqId
                );
        }

        // --------------------------------------------------------[]
        private static string RemoteFeedInprocessFolderPath( string feed )
        {
            return string.Format(
                "{0}{1}/inprocess",
                MipSettings.RemoteBasePath,
                feed
                );
        }

        // --------------------------------------------------------[]
        private static string[] RemoteFeedOutputFolderPathes( string feed )
        {
            return new[] {
                RemoteFeedOutputFolderPath( feed, 0 ),
                RemoteFeedOutputFolderPath( feed, -1 ),
                RemoteFeedOutputFolderPath( feed, 1 ),
                RemoteFeedOutputFolderPath( feed, -2 )
            };
        }

        // ===================================================================================== []
        // Remote Data Based Names
        private static string RemoteFeedOutputFolderPath( string feed, int dayShift )
        {
            return string.Format(
                "{0}{1}/output/{2}",
                MipSettings.RemoteBasePath,
                feed,
                DataBasedFolderName( dayShift )
                );
        }

        // ===================================================================================== []
        private static string DataBasedFolderName( int dayShift )
        {
            var mipNow = TimeZoneInfo.ConvertTimeBySystemTimeZoneId( DateTime.UtcNow, MipSettings.TimeZone );
            var dirTime = mipNow.AddHours( 24*dayShift );

            return dirTime.Date.ToString( "MMM-dd-yyy", CultureInfo.CreateSpecificCulture( "en-US" ) );
        }
    }
}