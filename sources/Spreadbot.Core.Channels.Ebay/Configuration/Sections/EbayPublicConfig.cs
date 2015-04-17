// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// EbayPublicConfig.cs

using System.Configuration;
using Krokodev.Common.SmartConfiguration;
using Spreadbot.Core.Channels.Ebay.Configuration.Elements;

namespace Spreadbot.Core.Channels.Ebay.Configuration.Sections
{
    [SectionName( "Spreadbot/EbayPublic" )]
    public class EbayPublicConfig : SmartConfigurationSection< EbayPublicConfig >
    {
        [ConfigurationProperty( "MipConnection", IsRequired = true )]
        public MipConnection MipConnection
        {
            get { return ( MipConnection ) this[ GetMethodName() ]; }
        }

        [ConfigurationProperty( "MipPaths", IsRequired = true )]
        public MipPaths MipPaths
        {
            get { return ( MipPaths ) this[ GetMethodName() ]; }
        }
    }
}