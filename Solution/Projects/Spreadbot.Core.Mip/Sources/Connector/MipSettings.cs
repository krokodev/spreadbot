using System;

namespace Spreadbot.Core.Mip
{
    static public class MipSettings
    {
        static MipSettings()
        {
            HostName = MipConfiguration.Instance.Connection.HostName;
            PortNumber = MipConfiguration.Instance.Connection.PortNumber;
            UserName = MipSecurityConfiguration.Instance.SecretData.UserName;
            Password = MipSecurityConfiguration.Instance.SecretData.Password;
        }

        public static string HostName { get; private set; }
        public static int PortNumber { get; private set; }
        public static string UserName { get; private set; }
        public static string Password { get; private set; }
    }
}