// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Stores
// DemoshopPaths.cs

using System.Configuration;
using Krokodev.Common.SmartConfiguration;

namespace Spreadbot.Core.Stores.Demoshop.Configuration.Elements
{
    public class DemoshopPaths : SmartConfigurationElement
    {
        [ConfigurationProperty( "XmlTemplatesPath", IsRequired = true )]
        public string XmlTemplatesPath
        {
            get { return ( string ) this[ GetPropertyName() ]; }
        }

        [ConfigurationProperty( "XmlDataFileName", IsRequired = true )]
        public string XmlDataFileName
        {
            get { return ( string ) this[ GetPropertyName() ]; }
        }
    }
}