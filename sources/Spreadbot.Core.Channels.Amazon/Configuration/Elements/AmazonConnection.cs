// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// AmazonConnection.cs

using System.Configuration;
using Krokodev.Common.SmartConfiguration;

namespace Spreadbot.Core.Channels.Amazon.Configuration.Elements
{
    public class AmazonConnection : SmartConfigurationElement
    {
        [ConfigurationProperty( "HostName", IsRequired = true )]
        public string HostName
        {
            get { return ( string ) this[ GetPropertyName() ]; }
        }
    }
}