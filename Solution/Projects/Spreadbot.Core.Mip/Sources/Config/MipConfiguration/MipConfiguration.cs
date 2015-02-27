using System.Configuration;
using Crocodev.Common.SmartConfiguration;

namespace Spreadbot.Core.Mip
{
    [SectionName("Spreadbot/Mip")]
    public class MipConfiguration : SmartConfigurationSection<MipConfiguration>
    {
        [ConfigurationProperty("Connection", IsRequired = true)]
        public MipConnection Connection
        {
            get { return (MipConnection)this[GetMethodName()]; }
        }
        [ConfigurationProperty("Paths", IsRequired = true)]
        public MipPaths Paths
        {
            get { return (MipPaths)this[GetMethodName()]; }
        }
    }

    public class MipConnection : SmartConfigurationElement
    {
        [ConfigurationProperty("HostName", IsRequired = true)]
        public string HostName
        {
            get { return (string)this[GetPropertyName()]; }
        }

        [ConfigurationProperty("PortNumber", IsRequired = true)]
        public int PortNumber
        {
            get { return (int)this[GetPropertyName()]; }
        }
    }


    public class MipPaths: SmartConfigurationElement
    {
        [ConfigurationProperty("ZippedFeedsPath", IsRequired = true)]
        public string ZippedFeedsPath
        {
            get { return (string)this[GetPropertyName()]; }
        }

        [ConfigurationProperty("RemoteBasePath", IsRequired = true)]
        public string RemoteBasePath
        {
            get { return (string)this[GetPropertyName()]; }
        }
    }
}