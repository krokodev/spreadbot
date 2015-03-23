﻿// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Stores
// DemoshopConfig.cs
// romak_000, 2015-03-21 2:11

using System.Configuration;
using Crocodev.Common.SmartConfiguration;
using Spreadbot.Core.Stores.Demoshop.Configuration.Elements;

namespace Spreadbot.Core.Stores.Demoshop.Configuration.Sections
{
    [SectionName( "Spreadbot/Demoshop" )]
    public class DemoshopConfig : SmartConfigurationSection< DemoshopConfig >
    {
        [ConfigurationProperty( "DemoshopPaths", IsRequired = true )]
        public DemoshopPaths DemoshopPaths
        {
            get { return ( DemoshopPaths ) this[ GetMethodName() ]; }
        }
    }
}