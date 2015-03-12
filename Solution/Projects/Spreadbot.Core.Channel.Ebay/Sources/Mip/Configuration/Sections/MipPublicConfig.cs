using System.Configuration;
using Crocodev.Common.SmartConfiguration;

namespace Spreadbot.Core.Channel.Ebay.Mip.Configuration
{
    [SectionName("Spreadbot/MipPublic")]
    public class MipPublicConfig : SmartConfigurationSection<MipPublicConfig>
    {
        [ConfigurationProperty("MipConnection", IsRequired = true)]
        public MipConnection MipConnection
        {
            get { return (MipConnection)this[GetMethodName()]; }
        }
        [ConfigurationProperty("MipPaths", IsRequired = true)]
        public MipPaths MipPaths
        {
            get { return (MipPaths)this[GetMethodName()]; }
        }
    }
}