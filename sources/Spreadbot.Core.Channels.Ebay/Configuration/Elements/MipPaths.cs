﻿// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// MipPaths.cs

using System.Configuration;
using Krokodev.Common.SmartConfiguration;

namespace Spreadbot.Core.Channels.Ebay.Configuration.Elements
{
    public class MipPaths : SmartConfigurationElement
    {
        [ConfigurationProperty( "BasePath", IsRequired = true )]
        public string BasePath
        {
            get { return ( string ) this[ GetPropertyName() ]; }
        }

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

        [ConfigurationProperty( "SftpServerTimeZone", IsRequired = true )]
        public string SftpServerTimeZone
        {
            get { return ( string ) this[ GetPropertyName() ]; }
        }
    }
}