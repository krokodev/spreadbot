// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// MipSecretData.cs
// Roman, 2015-04-07 2:57 PM

using System.Configuration;
using Crocodev.Common.SmartConfiguration;

namespace Spreadbot.Core.Channels.Ebay.Configuration.Elements
{
    public class MipSecretData : SmartConfigurationElement
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