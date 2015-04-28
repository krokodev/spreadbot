// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Amazon
// AmazonPublicConfig.cs

using System.Configuration;
using Krokodev.Common.SmartConfiguration;
using Spreadbot.Core.Channels.Amazon.Configuration.Elements;

namespace Spreadbot.Core.Channels.Amazon.Configuration.Sections
{
    [SectionName( "Spreadbot/AmazonPublic" )]
    public class AmazonPublicConfig : SmartConfigurationSection< AmazonPublicConfig >
    {
        [ConfigurationProperty( "MwsConnection", IsRequired = true )]
        public MwsConnection MwsConnection
        {
            get { return ( MwsConnection ) this[ GetMethodName() ]; }
        }

        [ConfigurationProperty( "MwsPaths", IsRequired = true )]
        public MwsPaths MwsPaths
        {
            get { return ( MwsPaths ) this[ GetMethodName() ]; }
        }
    }
}