// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Connectors.Ebay
// MipPublicConfig.cs
// romak_000, 2015-03-19 15:49

using System.Configuration;
using Crocodev.Common.SmartConfiguration;
using Spreadbot.Core.Connectors.Ebay.Configuration.Elements;

namespace Spreadbot.Core.Connectors.Ebay.Configuration.Sections
{
    [SectionName("Spreadbot/MipPublic")]
    public class MipPublicConfig : SmartConfigurationSection<MipPublicConfig>
    {
        [ConfigurationProperty("MipConnection", IsRequired = true)]
        public MipConnection MipConnection
        {
            get { return (MipConnection) this[GetMethodName()]; }
        }

        [ConfigurationProperty("MipPaths", IsRequired = true)]
        public MipPaths MipPaths
        {
            get { return (MipPaths) this[GetMethodName()]; }
        }
    }
}