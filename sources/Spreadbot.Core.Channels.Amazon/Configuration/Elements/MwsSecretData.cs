// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Amazon
// MwsSecretData.cs

using System.Configuration;
using Krokodev.Common.SmartConfiguration;

namespace Spreadbot.Core.Channels.Amazon.Configuration.Elements
{
    public class MwsSecretData : SmartConfigurationElement
    {
        [ConfigurationProperty( "MerchantId", IsRequired = true )]
        public string MerchantId
        {
            get { return ( string ) this[ GetPropertyName() ]; }
        }

        [ConfigurationProperty( "XmlMerchantIdentifier", IsRequired = true )]
        public string XmlMerchantIdentifier
        {
            get { return ( string ) this[ GetPropertyName() ]; }
        }

        [ConfigurationProperty( "AwsAccessKeyId", IsRequired = true )]
        public string AwsAccessKeyId
        {
            get { return ( string ) this[ GetPropertyName() ]; }
        }

        [ConfigurationProperty( "AwsSecretAccessKey", IsRequired = true )]
        public string AwsSecretAccessKey
        {
            get { return ( string ) this[ GetPropertyName() ]; }
        }
    }
}