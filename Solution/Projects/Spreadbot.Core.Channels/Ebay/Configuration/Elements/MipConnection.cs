// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// MipConnection.cs
// romak_000, 2015-03-21 2:11

using System.Configuration;
using Crocodev.Common.SmartConfiguration;

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