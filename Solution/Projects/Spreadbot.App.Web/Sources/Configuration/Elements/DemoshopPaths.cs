using System.Configuration;
using Crocodev.Common.SmartConfiguration;

namespace Spreadbot.App.Web.Configuration
{
    public class DemoshopPaths: SmartConfigurationElement
    {
        [ConfigurationProperty("XmlTemplatesPath", IsRequired = true)]
        public string XmlTemplatesPath
        {
            get { return (string)this[GetPropertyName()]; }
        }
        [ConfigurationProperty("XmlDataFileName", IsRequired = true)]
        public string XmlDataFileName
        {
            get { return (string)this[GetPropertyName()]; }
        }

    }
}