// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// MipSecurityConfig.cs
// Roman, 2015-04-10 1:28 PM

using System.Configuration;
using Crocodev.Common.SmartConfiguration;
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