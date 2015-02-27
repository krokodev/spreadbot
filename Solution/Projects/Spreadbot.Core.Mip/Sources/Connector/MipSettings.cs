using System;

namespace Spreadbot.Core.Mip
{
    public static class MipSettings
    {
        public static string HostName
        {
            get { return MipConfiguration.Instance.Connection.HostName; }
        }
        public static int PortNumber
        {
            get { return MipConfiguration.Instance.Connection.PortNumber; }
        }
        public static string UserName
        {
            get { return MipSecurityConfiguration.Instance.SecretData.UserName; }
        }
        public static string Password
        {
            get { return MipSecurityConfiguration.Instance.SecretData.Password; }
        }
        public static string ZippedFeedsPath
        {
            get
            {
                return MipConfiguration.Instance.Paths.ZippedFeedsPath; 
            }
        }
        public static string RemoteBasePath
        {
            get
            {
                return MipConfiguration.Instance.Paths.RemoteBasePath; 
            }
        }
    }
}