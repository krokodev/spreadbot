﻿// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Stores
// DemoshopConfig.cs

using System.Configuration;
using Krokodev.Common.SmartConfiguration;
using Spreadbot.Core.Stores.Demoshop.Configuration.Elements;

namespace Spreadbot.Core.Stores.Demoshop.Configuration.Sections
{
    [SectionName( "Spreadbot/Demoshop" )]
    public class DemoshopConfig : SmartConfigurationSection< DemoshopConfig >
    {
        [ConfigurationProperty( "DemoshopPaths", IsRequired = false )]
        public DemoshopPaths DemoshopPaths
        {
            get { return ( DemoshopPaths ) this[ GetMethodName() ]; }
        }
    }
}