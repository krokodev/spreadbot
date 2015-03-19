// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Connectors.Ebay
// MipSettings.cs
// romak_000, 2015-03-19 15:49

using System;
using Spreadbot.Core.Connectors.Ebay.Configuration.Sections;

namespace Spreadbot.Core.Connectors.Ebay.Mip.Settings
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
            get { return MapToDataDirectory(MipPublicConfig.Instance.MipPaths.ZippedFeedsPath); }
        }

        public static string FeedsPath
        {
            get { return MapToDataDirectory(MipPublicConfig.Instance.MipPaths.FeedsPath); }
        }

        public static string InboxPath
        {
            get { return MapToDataDirectory(MipPublicConfig.Instance.MipPaths.InboxPath); }
        }

        // ===================================================================================== []
        // Remote pathes
        public static string RemoteBasePath
        {
            get { return MipPublicConfig.Instance.MipPaths.RemoteBasePath; }
        }

        // --------------------------------------------------------[]
        public static int OutputFolderNameUtcHourOffset
        {
            get { return MipPublicConfig.Instance.MipPaths.OutputFolderNameUtcHourOffset; }
        }

        // ===================================================================================== []
        // Map
        private static string MapToDataDirectory(string path)
        {
            return AppDomain.CurrentDomain.GetData("DataDirectory") + path;
        }
    }
}