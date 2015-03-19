// Spreadbot (c) 2015 Crocodev
// Spreadbot.App.Web
// DemoshopConfig.cs
// romak_000, 2015-03-19 15:37

using System.Configuration;
using Crocodev.Common.SmartConfiguration;
using Spreadbot.App.Web.Sources.Configuration.Elements;

namespace Spreadbot.App.Web.Sources.Configuration.Sections
{
    [SectionName("Spreadbot/Demoshop")]
    public class DemoshopConfig : SmartConfigurationSection<DemoshopConfig>
    {
        [ConfigurationProperty("DemoshopPaths", IsRequired = true)]
        public DemoshopPaths DemoshopPaths
        {
            get { return (DemoshopPaths) this[GetMethodName()]; }
        }
    }
}