using System.Configuration;
using Crocodev.Common.SmartConfiguration;

namespace Spreadbot.App.Web.Configuration
{
    public class Paths: SmartConfigurationElement
    {
        [ConfigurationProperty("XmlTemplatesPath", IsRequired = true)]
        public string XmlTemplatesPath
        {
            get { return (string)this[GetPropertyName()]; }
        }
    }
}