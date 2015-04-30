// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// MipConnector.pvt.Pathes.cs

using System;
using System.Globalization;
using Spreadbot.Core.Channels.Ebay.Configuration.Settings;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Feed;

namespace Spreadbot.Core.Channels.Ebay.Services.Mip.Connector
{
    public partial class MipConnector
    {
        // ===================================================================================== []
        // Local
        public static string LocalZippedFeedFile( string feed, string reqId )
        {
            return string.Format(
                @"{0}\{1}.{2}.zip",
                EbaySettings.ZippedFeedsPath,
                feed,
                reqId
                );
        }

        // --------------------------------------------------------[]
        private static string _LocalFeedFolder( string feed )
        {
            return string.Format(
                "{0}{1}",
                EbaySettings.FeedsPath,
                feed
                );
        }

        // --------------------------------------------------------[]
        private static string LocalSubmissionResultsFolder()
        {
            return EbaySettings.InboxPath;
        }

        // --------------------------------------------------------[]
        private static string _LocalFeedXmlFilePath( MipFeedDescriptor mipFeedDescriptor )
        {
            return string.Format(
                @"{0}\{1}.{2}.xml",
                _LocalFeedFolder( mipFeedDescriptor.GetName() ),
                mipFeedDescriptor.GetName(),
                mipFeedDescriptor.Id
                );
        }

        // ===================================================================================== []
        // Remote
        public static string RemoteFeedOutgoingZipFilePath( string feed, string reqId )
        {
            return string.Format(
                "{0}{1}/{1}.{2}.zip",
                EbaySettings.RemoteBasePath,
                feed,
                reqId
                );
        }

        // --------------------------------------------------------[]
        private static string RemoteFeedInprocessFolderPath( string feed )
        {
            return string.Format(
                "{0}{1}/inprocess",
                EbaySettings.RemoteBasePath,
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
                EbaySettings.RemoteBasePath,
                feed,
                DataBasedFolderName( dayShift )
                );
        }

        // ===================================================================================== []
        private static string DataBasedFolderName( int dayShift )
        {
            var mipNow = TimeZoneInfo.ConvertTimeBySystemTimeZoneId( DateTime.UtcNow, EbaySettings.TimeZone );
            var dirTime = mipNow.AddHours( 24*dayShift );

            return dirTime.Date.ToString( "MMM-dd-yyy", CultureInfo.CreateSpecificCulture( "en-US" ) );
        }
    }
}