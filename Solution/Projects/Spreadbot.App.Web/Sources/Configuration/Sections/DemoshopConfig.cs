using System.Configuration;
using Crocodev.Common.SmartConfiguration;

namespace Spreadbot.App.Web.Configuration
{
    [SectionName("Spreadbot/Demoshop")]
    public class DemoshopConfig : SmartConfigurationSection<DemoshopConfig>
    {
        [ConfigurationProperty("DemoshopPaths", IsRequired = true)]
        public DemoshopPaths DemoshopPaths
        {
            get { return (DemoshopPaths)this[GetMethodName()]; }
        }
    }
}