// Spreadbot (c) 2015 Crocodev
// Spreadbot.App.Web
// DemoshopPaths.cs
// romak_000, 2015-03-19 15:49

using System.Configuration;
using Crocodev.Common.SmartConfiguration;

namespace Spreadbot.App.Web.Sources.Configuration.Elements
{
    public class DemoshopPaths : SmartConfigurationElement
    {
        [ConfigurationProperty("XmlTemplatesPath", IsRequired = true)]
        public string XmlTemplatesPath
        {
            get { return (string) this[GetPropertyName()]; }
        }

        [ConfigurationProperty("XmlDataFileName", IsRequired = true)]
        public string XmlDataFileName
        {
            get { return (string) this[GetPropertyName()]; }
        }
    }
}