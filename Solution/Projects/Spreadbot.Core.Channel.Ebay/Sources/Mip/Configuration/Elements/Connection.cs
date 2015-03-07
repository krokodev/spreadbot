using System.Configuration;
using Crocodev.Common.SmartConfiguration;

namespace Spreadbot.Core.Channel.Ebay.Mip.Configuration
{
    public class Connection : SmartConfigurationElement
    {
        [ConfigurationProperty("HostName", IsRequired = true)]
        public string HostName
        {
            get { return (string)this[GetPropertyName()]; }
        }

        [ConfigurationProperty("PortNumber", IsRequired = true)]
        public int PortNumber
        {
            get { return (int)this[GetPropertyName()]; }
        }
    }
}