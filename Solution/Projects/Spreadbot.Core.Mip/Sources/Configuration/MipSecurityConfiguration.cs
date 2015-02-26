using System;
using System.Configuration;
using System.Threading;
using Crocodev.Common;

namespace Spreadbot.Core.Mip
{
    // Now: MipConfiguration
    public class MiSecuritypConfiguration : SmartConfigurationSection
    {
        private static Lazy<MipConfiguration> _instance = new Lazy<MipConfiguration>(CreateInstance,
            LazyThreadSafetyMode.ExecutionAndPublication);

        public static MipConfiguration Instance
        {
            get { return _instance.Value; }
        }

        private static MipConfiguration CreateInstance()
        {
            return (MipConfiguration)ConfigurationManager.GetSection("Spreadbot/MipSecurity");
        }

        [ConfigurationProperty("Connection", IsRequired = true)]
        public MipConnection Connection
        {
            get { return (MipConnection)this[GetPropertyName()]; }
        }

        [ConfigurationProperty("Security", IsRequired = true)]
        public SecurityConfiguration Security
        {
            get { return (SecurityConfiguration)this[GetPropertyName()]; }
        }
    }

    public class MipConnection : SmartConfigurationElement
    {
        [ConfigurationProperty("HostName", IsRequired = true)]
        public string HostName
        {
            get { return (string)this[GetPropertyName()]; }
            set { this[GetPropertyName()] = value; }
        }

        [ConfigurationProperty("PortNumber", IsRequired = true)]
        public int PortNumber
        {
            get { return (int)this[GetPropertyName()]; }
            set { this[GetPropertyName()] = value; }
        }
    }

    public class SecurityConfiguration : SmartConfigurationElement
    {
        [ConfigurationProperty("UserName", IsRequired = true)]
        public string UserName
        {
            get { return (string)this[GetPropertyName()]; }
            set { this[GetPropertyName()] = value; }
        }

        [ConfigurationProperty("Password", IsRequired = true)]
        public string Password
        {
            get { return (string)this[GetPropertyName()]; }
            set { this[GetPropertyName()] = value; }
        }
    }
}