// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// MipSecurityConfig.cs

using System.Configuration;
using Krokodev.Common.SmartConfiguration;
using Spreadbot.Core.Channels.Ebay.Configuration.Elements;

namespace Spreadbot.Core.Channels.Ebay.Configuration.Sections
{
    [SectionName( "Spreadbot/MipSecurity" )]
    public class MipSecurityConfig : SmartConfigurationSection< MipSecurityConfig >
    {
        [ConfigurationProperty( "MipSecretData", IsRequired = true )]
        public MipSecretData MipSecretData
        {
            get { return ( MipSecretData ) this[ GetMethodName() ]; }
        }
    }
}