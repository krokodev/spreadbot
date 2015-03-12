using System.Configuration;
using Crocodev.Common.SmartConfiguration;

namespace Spreadbot.Core.Channel.Ebay.Mip.Configuration
{
    [SectionName("Spreadbot/MipSecurity")]
    public class MipSecurityConfig : SmartConfigurationSection<MipSecurityConfig>
    {
        [ConfigurationProperty("MipSecretData", IsRequired = true)]
        public MipSecretData MipSecretData
        {
            get { return (MipSecretData)this[GetMethodName()]; }
        }
    }
}