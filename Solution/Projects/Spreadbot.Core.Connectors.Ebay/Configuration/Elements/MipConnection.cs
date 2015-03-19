// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Connectors.Ebay
// MipConnection.cs
// romak_000, 2015-03-19 15:38

using System.Configuration;
using Crocodev.Common.SmartConfiguration;

namespace Spreadbot.Core.Channel.Ebay.Configuration.Elements
{
    public class MipConnection : SmartConfigurationElement
    {
        [ConfigurationProperty("HostName", IsRequired = true)]
        public string HostName
        {
            get { return (string) this[GetPropertyName()]; }
        }

        [ConfigurationProperty("PortNumber", IsRequired = true)]
        public int PortNumber
        {
            get { return (int) this[GetPropertyName()]; }
        }
    }
}