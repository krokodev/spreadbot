﻿using System.Configuration;
using Crocodev.Common.SmartConfiguration;

namespace Spreadbot.Core.Mip
{
    [SectionName("Spreadbot/MipSecurity")]
    public class MipSecurityConfiguration : SmartConfigurationSection<MipSecurityConfiguration>
    {
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