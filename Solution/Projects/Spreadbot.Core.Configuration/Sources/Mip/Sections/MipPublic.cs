﻿using System.Configuration;
using Crocodev.Common.SmartConfiguration;

namespace Spreadbot.Core.Configuration
{
    [SectionName("Spreadbot/MipPublic")]
    public class MipPublic : SmartConfigurationSection<MipPublic>
    {
        [ConfigurationProperty("Connection", IsRequired = true)]
        public Connection Connection
        {
            get { return (Connection)this[GetMethodName()]; }
        }
        [ConfigurationProperty("Paths", IsRequired = true)]
        public Paths Paths
        {
            get { return (Paths)this[GetMethodName()]; }
        }
    }
}