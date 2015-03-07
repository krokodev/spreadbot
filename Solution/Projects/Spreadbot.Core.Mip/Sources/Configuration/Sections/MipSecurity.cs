using System.Configuration;
using Crocodev.Common.SmartConfiguration;

namespace Spreadbot.Core.Mip.Configuration
{
    [SectionName("Spreadbot/MipSecurity")]
    public class MipSecurity : SmartConfigurationSection<MipSecurity>
    {
        [ConfigurationProperty("SecretData", IsRequired = true)]
        public SecretData SecretData
        {
            get { return (SecretData)this[GetMethodName()]; }
        }
    }
}