// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// AmazonSecurityConfig.cs

using System.Configuration;
using Krokodev.Common.SmartConfiguration;
using Spreadbot.Core.Channels.Amazon.Configuration.Elements;

namespace Spreadbot.Core.Channels.Amazon.Configuration.Sections
{
    [SectionName( "Spreadbot/MipSecurity" )]
    public class AmazonSecurityConfig : SmartConfigurationSection< AmazonSecurityConfig >
    {
        [ConfigurationProperty( "AmazonSecretData", IsRequired = true )]
        public AmazonSecretData AmazonSecretData
        {
            get { return ( AmazonSecretData ) this[ GetMethodName() ]; }
        }
    }
}