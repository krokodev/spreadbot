using Spreadbot.Core.Configuration;

namespace Spreadbot.Core.Mip
{
    public static class Settings
    {
        public static string HostName
        {
            get { return MipPublic.Instance.Connection.HostName; }
        }
        public static int PortNumber
        {
            get { return MipPublic.Instance.Connection.PortNumber; }
        }
        public static string UserName
        {
            get { return MipSecurity.Instance.SecretData.UserName; }
        }
        public static string Password
        {
            get { return MipSecurity.Instance.SecretData.Password; }
        }
        public static string ZippedFeedsPath
        {
            get
            {
                return MipPublic.Instance.Paths.ZippedFeedsPath; 
            }
        }
        public static string RemoteBasePath
        {
            get
            {
                return MipPublic.Instance.Paths.RemoteBasePath; 
            }
        }

        public static string FeedsPath
        {
            get
            {
                return MipPublic.Instance.Paths.FeedsPath;
            }
        }
    }
}