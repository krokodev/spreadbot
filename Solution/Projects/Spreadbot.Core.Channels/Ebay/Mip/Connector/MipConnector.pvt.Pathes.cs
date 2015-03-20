// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// MipConnector.pvt.Pathes.cs
// romak_000, 2015-03-20 13:56

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
        private static string LocalZippedFeedFile( string feed, string reqId )
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
                                 DoLocalFeedFolder( mipFeedHandler.Name ),
                                 mipFeedHandler.Name,
                                 ( Guid ) mipFeedHandler.Id
                );
        }

        // ===================================================================================== []
        // Remote
        private static string RemoteFeedOutgoingZipFilePath( string feed, string reqId )
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
            var hourOffset = MipSettings.OutputFolderNameUtcHourOffset;
            var utcNow = DateTime.UtcNow;
            var mipNow = utcNow.AddHours( hourOffset + 24*dayShift );

            return mipNow.Date.ToString( "MMM-dd-yyy", CultureInfo.CreateSpecificCulture( "en-US" ) );
        }
    }
}