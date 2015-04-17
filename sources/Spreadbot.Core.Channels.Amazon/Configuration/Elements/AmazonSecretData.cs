// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// AmazonSecretData.cs

using System.Configuration;
using Krokodev.Common.SmartConfiguration;

namespace Spreadbot.Core.Channels.Amazon.Configuration.Elements
{
    public class AmazonSecretData : SmartConfigurationElement
    {
        [ConfigurationProperty( "UserName", IsRequired = true )]
        public string UserName
        {
            get { return ( string ) this[ GetPropertyName() ]; }
        }

        [ConfigurationProperty( "Password", IsRequired = true )]
        public string Password
        {
            get { return ( string ) this[ GetPropertyName() ]; }
        }
    }
}