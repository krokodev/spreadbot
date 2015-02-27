using System;
using System.Configuration;
using System.Threading;
using Crocodev.Common.SmartConfiguration;

namespace Spreadbot.Core.Mip
{
    [SectionName("Spreadbot/Mip")]
    public class MipConfiguration : SmartConfigurationSection<MipConfiguration>
    {
/*        private static readonly Lazy<MipConfiguration> InstanceValue = new Lazy<MipConfiguration>(CreateInstance,
            LazyThreadSafetyMode.ExecutionAndPublication);

        public static MipConfiguration Instance
        {
            get { return InstanceValue.Value; }
        }

        private static MipConfiguration CreateInstance()
        {
            return (MipConfiguration) ConfigurationManager.GetSection("Spreadbot/Mip");
        }*/

        [ConfigurationProperty("Connection", IsRequired = true)]
        public MipConnection Connection
        {
            get { return (MipConnection)this[GetPropertyName()]; }
        }
    }

    public class MipConnection : SmartConfigurationElement
    {
        [ConfigurationProperty("HostName", IsRequired = true)]
        public string HostName
        {
            get { return (string)this[GetPropertyName()]; }
            set { this[GetPropertyName()] = value; }
        }

        [ConfigurationProperty("PortNumber", IsRequired = true)]
        public int PortNumber
        {
            get { return (int)this[GetPropertyName()]; }
            set { this[GetPropertyName()] = value; }
        }
    }
}