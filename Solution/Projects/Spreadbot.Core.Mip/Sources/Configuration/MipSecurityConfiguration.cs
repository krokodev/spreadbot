using System;
using System.Configuration;
using System.Threading;
using Crocodev.Common;

namespace Spreadbot.Core.Mip
{
    public class MipSecurityConfiguration : SmartConfigurationSection
    {
        private static Lazy<MipSecurityConfiguration> _instance = new Lazy<MipSecurityConfiguration>(CreateInstance,
            LazyThreadSafetyMode.ExecutionAndPublication);

        public static MipSecurityConfiguration Instance
        {
            get { return _instance.Value; }
        }

        private static MipSecurityConfiguration CreateInstance()
        {
            return (MipSecurityConfiguration)ConfigurationManager.GetSection("Spreadbot/MipSecurity");
        }

        [ConfigurationProperty("SecretData", IsRequired = true)]
        public MipSecretData SecretData
        {
            get { return (MipSecretData)this[GetPropertyName()]; }
        }
    }

    public class MipSecretData : SmartConfigurationElement
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