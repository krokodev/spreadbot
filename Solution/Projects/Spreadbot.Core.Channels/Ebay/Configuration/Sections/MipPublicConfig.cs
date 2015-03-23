﻿// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// MipPublicConfig.cs
// romak_000, 2015-03-21 2:11

using System.Configuration;
using Crocodev.Common.SmartConfiguration;
using Spreadbot.Core.Channels.Ebay.Configuration.Elements;

namespace Spreadbot.Core.Channels.Ebay.Configuration.Sections
{
    [SectionName( "Spreadbot/MipPublic" )]
    public class MipPublicConfig : SmartConfigurationSection< MipPublicConfig >
    {
        [ConfigurationProperty( "MipConnection", IsRequired = true )]
        public MipConnection MipConnection
        {
            get { return ( MipConnection ) this[ GetMethodName() ]; }
        }

        [ConfigurationProperty( "MipPaths", IsRequired = true )]
        public MipPaths MipPaths
        {
            get { return ( MipPaths ) this[ GetMethodName() ]; }
        }
    }
}