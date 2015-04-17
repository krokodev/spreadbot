// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Amazon
// AmazonSecretConfig.cs

using System.Configuration;
using Krokodev.Common.SmartConfiguration;
using Spreadbot.Core.Channels.Amazon.Configuration.Elements;

namespace Spreadbot.Core.Channels.Amazon.Configuration.Sections
{
    [SectionName( "Spreadbot/AmazonSecret" )]
    public class AmazonSecretConfig : SmartConfigurationSection< AmazonSecretConfig >
    {
        [ConfigurationProperty( "MwsSecretData", IsRequired = true )]
        public MwsSecretData MwsSecretData
        {
            get { return ( MwsSecretData ) this[ GetMethodName() ]; }
        }
    }
}