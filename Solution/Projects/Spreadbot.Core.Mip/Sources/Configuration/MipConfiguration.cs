using System;
using System.Configuration;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Spreadbot.Core.Mip
{
    // Now: MipConfiguration
    public class MipConfiguration : SmartConfigurationSection
    {
        private static Lazy<MipConfiguration> _instance = new Lazy<MipConfiguration>(CreateInstance,
            LazyThreadSafetyMode.ExecutionAndPublication);

        public static MipConfiguration Instance
        {
            get { return _instance.Value; }
        }

        private static MipConfiguration CreateInstance()
        {
            return (MipConfiguration) ConfigurationManager.GetSection("Spreadbot/Mip");
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

    public class SmartConfigurationElement : ConfigurationElement
    {
        protected static string GetPropertyName([CallerMemberName] string name = "")
        {
            return name;
        }
    }

    public class SmartConfigurationSection : ConfigurationSection
    {
        protected static string GetPropertyName([CallerMemberName] string name = "")
        {
            return name;
        }
    }
}