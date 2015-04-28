// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Amazon
// MwsConnection.cs

using System.Configuration;
using Krokodev.Common.SmartConfiguration;

namespace Spreadbot.Core.Channels.Amazon.Configuration.Elements
{
    public class MwsConnection : SmartConfigurationElement
    {
        [ConfigurationProperty( "ServiceUrl", IsRequired = true )]
        public string ServiceUrl
        {
            get { return ( string ) this[ GetPropertyName() ]; }
        }

        [ConfigurationProperty( "MarketplaceId", IsRequired = true )]
        public string MarketplaceId
        {
            get { return ( string ) this[ GetPropertyName() ]; }
        }
    }
}