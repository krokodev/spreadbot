// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Connectors.Ebay
// MipSecurityConfig.cs
// romak_000, 2015-03-19 14:02

using System.Configuration;
using Crocodev.Common.SmartConfiguration;
using Spreadbot.Core.Channel.Ebay.Configuration.Elements;

namespace Spreadbot.Core.Channel.Ebay.Configuration.Sections
{
    [SectionName("Spreadbot/MipSecurity")]
    public class MipSecurityConfig : SmartConfigurationSection<MipSecurityConfig>
    {
        [ConfigurationProperty("MipSecretData", IsRequired = true)]
        public MipSecretData MipSecretData
        {
            get { return (MipSecretData) this[GetMethodName()]; }
        }
    }
}