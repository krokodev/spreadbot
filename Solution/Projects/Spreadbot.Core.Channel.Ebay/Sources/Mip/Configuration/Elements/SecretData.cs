using System.Configuration;
using Crocodev.Common.SmartConfiguration;

namespace Spreadbot.Core.Channel.Ebay.Mip.Configuration
{
    public class SecretData : SmartConfigurationElement
    {
        [ConfigurationProperty("UserName", IsRequired = true)]
        public string UserName
        {
            get { return (string)this[GetPropertyName()]; }
        }

        [ConfigurationProperty("Password", IsRequired = true)]
        public string Password
        {
            get { return (string)this[GetPropertyName()]; }
        }
    }
}