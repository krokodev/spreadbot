// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Amazon
// MwsPaths.cs

using System.Configuration;
using Krokodev.Common.SmartConfiguration;

namespace Spreadbot.Core.Channels.Amazon.Configuration.Elements
{
    public class MwsPaths : SmartConfigurationElement
    {
        [ConfigurationProperty( "BasePath", IsRequired = true )]
        public string BasePath
        {
            get { return ( string ) this[ GetPropertyName() ]; }
        }

        [ConfigurationProperty( "FeedsPath", IsRequired = true )]
        public string FeedsPath
        {
            get { return ( string ) this[ GetPropertyName() ]; }
        }
    }
}