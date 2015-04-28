// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// EbaySettings.cs

using Krokodev.Common.Extensions;
using Spreadbot.Core.Channels.Ebay.Configuration.Sections;

namespace Spreadbot.Core.Channels.Ebay.Configuration.Settings
{
    public static class EbaySettings
    {
        public static string HostName
        {
            get { return EbayPublicConfig.Instance.MipConnection.HostName; }
        }

        public static int PortNumber
        {
            get { return EbayPublicConfig.Instance.MipConnection.PortNumber; }
        }

        public static string UserName
        {
            get { return EbaySecretConfig.Instance.MipSecretData.UserName; }
        }

        public static string Password
        {
            get { return EbaySecretConfig.Instance.MipSecretData.Password; }
        }

        public static string ZippedFeedsPath
        {
            get { return MapToDataDirectory( EbayPublicConfig.Instance.MipPaths.ZippedFeedsPath ); }
        }

        public static string FeedsPath
        {
            get { return MapToDataDirectory( EbayPublicConfig.Instance.MipPaths.FeedsPath ); }
        }

        public static string InboxPath
        {
            get { return MapToDataDirectory( EbayPublicConfig.Instance.MipPaths.InboxPath ); }
        }

        public static string LocalBasePath
        {
            get { return MapToDataDirectory( EbayPublicConfig.Instance.MipPaths.BasePath ); }
        }

        public static string RemoteBasePath
        {
            get { return EbayPublicConfig.Instance.MipPaths.RemoteBasePath; }
        }

        public static string TimeZone
        {
            get { return EbayPublicConfig.Instance.MipPaths.SftpServerTimeZone; }
        }

        // ===================================================================================== []
        // Utils
        private static string MapToDataDirectory( string path )
        {
            return path.MapPathToDataDirectory();
        }
    }
}