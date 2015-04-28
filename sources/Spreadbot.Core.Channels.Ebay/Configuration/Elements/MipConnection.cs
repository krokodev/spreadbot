// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// MipConnection.cs

using System.Configuration;
using Krokodev.Common.SmartConfiguration;

namespace Spreadbot.Core.Channels.Ebay.Configuration.Elements
{
    public class MipConnection : SmartConfigurationElement
    {
        [ConfigurationProperty( "HostName", IsRequired = true )]
        public string HostName
        {
            get { return ( string ) this[ GetPropertyName() ]; }
        }

        [ConfigurationProperty( "PortNumber", IsRequired = true )]
        public int PortNumber
        {
            get { return ( int ) this[ GetPropertyName() ]; }
        }
    }
}