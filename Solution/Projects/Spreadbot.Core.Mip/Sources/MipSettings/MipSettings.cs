namespace Spreadbot.Core.Mip
{
    public static class MipSettings
    {
        public static string HostName
        {
            get { return Configuration.Mip.Instance.Connection.HostName; }
        }
        public static int PortNumber
        {
            get { return Configuration.Mip.Instance.Connection.PortNumber; }
        }
        public static string UserName
        {
            get { return Configuration.MipSecurity.Instance.SecretData.UserName; }
        }
        public static string Password
        {
            get { return Configuration.MipSecurity.Instance.SecretData.Password; }
        }
        public static string ZippedFeedsPath
        {
            get
            {
                return Configuration.Mip.Instance.Paths.ZippedFeedsPath; 
            }
        }
        public static string RemoteBasePath
        {
            get
            {
                return Configuration.Mip.Instance.Paths.RemoteBasePath; 
            }
        }
    }
}