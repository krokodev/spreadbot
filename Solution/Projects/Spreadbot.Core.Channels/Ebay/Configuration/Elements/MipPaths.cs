// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// MipPaths.cs
// romak_000, 2015-03-20 13:56

using System.Configuration;
using Crocodev.Common.SmartConfiguration;

namespace Spreadbot.Core.Channels.Ebay.Configuration.Elements
{
    public class MipPaths : SmartConfigurationElement
    {
        [ConfigurationProperty( "ZippedFeedsPath", IsRequired = true )]
        public string ZippedFeedsPath
        {
            get { return ( string ) this[ GetPropertyName() ]; }
        }

        [ConfigurationProperty( "FeedsPath", IsRequired = true )]
        public string FeedsPath
        {
            get { return ( string ) this[ GetPropertyName() ]; }
        }

        [ConfigurationProperty( "RemoteBasePath", IsRequired = true )]
        public string RemoteBasePath
        {
            get { return ( string ) this[ GetPropertyName() ]; }
        }

        [ConfigurationProperty( "InboxPath", IsRequired = true )]
        public string InboxPath
        {
            get { return ( string ) this[ GetPropertyName() ]; }
        }

        [ConfigurationProperty( "OutputFolderNameUtcHourOffset", IsRequired = true )]
        public int OutputFolderNameUtcHourOffset
        {
            get { return ( int ) this[ GetPropertyName() ]; }
        }
    }
}