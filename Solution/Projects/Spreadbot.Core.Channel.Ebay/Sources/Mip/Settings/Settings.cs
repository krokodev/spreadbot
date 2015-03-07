using System;
using Spreadbot.Core.Channel.Ebay.Mip.Configuration;

namespace Spreadbot.Core.Channel.Ebay.Mip
{
    public static class Settings
    {
        // ===================================================================================== []
        // Mip server
        public static string HostName
        {
            get { return MipPublic.Instance.Connection.HostName; }
        }

        // --------------------------------------------------------[]
        public static int PortNumber
        {
            get { return MipPublic.Instance.Connection.PortNumber; }
        }

        // --------------------------------------------------------[]
        public static string UserName
        {
            get { return MipSecurity.Instance.SecretData.UserName; }
        }

        // --------------------------------------------------------[]
        public static string Password
        {
            get { return MipSecurity.Instance.SecretData.Password; }
        }

        // ===================================================================================== []
        // Local pathes
        public static string ZippedFeedsPath
        {
            get { return Map(MipPublic.Instance.Paths.ZippedFeedsPath); }
        }

        public static string FeedsPath
        {
            get { return Map(MipPublic.Instance.Paths.FeedsPath); }
        }

        public static string InboxPath
        {
            get { return Map(MipPublic.Instance.Paths.InboxPath); }
        }

        // ===================================================================================== []
        // Remote pathes
        public static string RemoteBasePath
        {
            get { return MipPublic.Instance.Paths.RemoteBasePath; }
        }

        // --------------------------------------------------------[]
        public static int OutputFolderNameUtcHourOffset
        {
            get { return MipPublic.Instance.Paths.OutputFolderNameUtcHourOffset; }
        }

        // ===================================================================================== []
        // Map
        private static string Map(string path)
        {
            return AppDomain.CurrentDomain.GetData("DataDirectory") + path;
        }
    }
}