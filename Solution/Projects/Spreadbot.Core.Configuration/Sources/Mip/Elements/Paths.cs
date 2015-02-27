using System.Configuration;
using Crocodev.Common.SmartConfiguration;

namespace Spreadbot.Core.Configuration
{
    public class Paths: SmartConfigurationElement
    {
        [ConfigurationProperty("ZippedFeedsPath", IsRequired = true)]
        public string ZippedFeedsPath
        {
            get { return (string)this[GetPropertyName()]; }
        }

        [ConfigurationProperty("FeedsPath", IsRequired = true)]
        public string FeedsPath
        {
            get { return (string)this[GetPropertyName()]; }
        }

        [ConfigurationProperty("RemoteBasePath", IsRequired = true)]
        public string RemoteBasePath
        {
            get { return (string)this[GetPropertyName()]; }
        }

        [ConfigurationProperty("InboxPath", IsRequired = true)]
        public string InboxPath
        {
            get { return (string)this[GetPropertyName()]; }
        }

    }
}