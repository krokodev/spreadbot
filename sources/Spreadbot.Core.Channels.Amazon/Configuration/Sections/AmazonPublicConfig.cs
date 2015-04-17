// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// AmazonPublicConfig.cs

using System.Configuration;
using Krokodev.Common.SmartConfiguration;
using Spreadbot.Core.Channels.Amazon.Configuration.Elements;

namespace Spreadbot.Core.Channels.Amazon.Configuration.Sections
{
    [SectionName( "Spreadbot/MipPublic" )]
    public class AmazonPublicConfig : SmartConfigurationSection< AmazonPublicConfig >
    {
        [ConfigurationProperty( "AmazonConnection", IsRequired = true )]
        public AmazonConnection AmazonConnection
        {
            get { return ( AmazonConnection ) this[ GetMethodName() ]; }
        }

/*
            [ConfigurationProperty( "MipPaths", IsRequired = true )]
            public MipPaths MipPaths
            {
                get { return ( MipPaths ) this[ GetMethodName() ]; }
            }
*/
    }
}