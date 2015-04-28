// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// EbaySecretConfig.cs

using System.Configuration;
using Krokodev.Common.SmartConfiguration;
using Spreadbot.Core.Channels.Ebay.Configuration.Elements;

namespace Spreadbot.Core.Channels.Ebay.Configuration.Sections
{
    [SectionName( "Spreadbot/EbaySecret" )]
    public class EbaySecretConfig : SmartConfigurationSection< EbaySecretConfig >
    {
        [ConfigurationProperty( "MipSecretData", IsRequired = true )]
        public MipSecretData MipSecretData
        {
            get { return ( MipSecretData ) this[ GetMethodName() ]; }
        }
    }
}