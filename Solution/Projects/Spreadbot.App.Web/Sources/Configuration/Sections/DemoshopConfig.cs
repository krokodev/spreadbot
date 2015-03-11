using System.Configuration;
using Crocodev.Common.SmartConfiguration;

namespace Spreadbot.App.Web.Configuration
{
    [SectionName("Spreadbot/Demoshop")]
    public class DemoshopConfig : SmartConfigurationSection<DemoshopConfig>
    {
        [ConfigurationProperty("Paths", IsRequired = true)]
        public Paths Paths
        {
            get { return (Paths)this[GetMethodName()]; }
        }
    }
}