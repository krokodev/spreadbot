// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// MipSettings.cs
// romak_000, 2015-03-26 19:42

using Crocodev.Common.Extensions;
using Spreadbot.Core.Channels.Ebay.Configuration.Sections;

namespace Spreadbot.Core.Channels.Ebay.Mip.Settings
{
    public static class MipSettings
    {
        // ===================================================================================== []
        // Mip server
        public static string HostName
        {
            get { return MipPublicConfig.Instance.MipConnection.HostName; }
        }

        // --------------------------------------------------------[]
        public static int PortNumber
        {
            get { return MipPublicConfig.Instance.MipConnection.PortNumber; }
        }

        // --------------------------------------------------------[]
        public static string UserName
        {
            get { return MipSecurityConfig.Instance.MipSecretData.UserName; }
        }

        // --------------------------------------------------------[]
        public static string Password
        {
            get { return MipSecurityConfig.Instance.MipSecretData.Password; }
        }

        // ===================================================================================== []
        // Local pathes
        public static string ZippedFeedsPath
        {
            get { return MapToDataDirectory( MipPublicConfig.Instance.MipPaths.ZippedFeedsPath ); }
        }

        // --------------------------------------------------------[]
        public static string FeedsPath
        {
            get { return MapToDataDirectory( MipPublicConfig.Instance.MipPaths.FeedsPath ); }
        }

        // --------------------------------------------------------[]
        public static string InboxPath
        {
            get { return MapToDataDirectory( MipPublicConfig.Instance.MipPaths.InboxPath ); }
        }

        // --------------------------------------------------------[]
        public static string LocalBasePath
        {
            get { return MapToDataDirectory( MipPublicConfig.Instance.MipPaths.BasePath ); }
        }

        // ===================================================================================== []
        // Remote pathes
        public static string RemoteBasePath
        {
            get { return MipPublicConfig.Instance.MipPaths.RemoteBasePath; }
        }

        // --------------------------------------------------------[]
        public static string TimeZone
        {
            get { return MipPublicConfig.Instance.MipPaths.SftpServerTimeZone; }
        }

        // ===================================================================================== []
        // Map
        private static string MapToDataDirectory( string path )
        {
            return path.MapPathToDataDirectory();
        }
    }
}