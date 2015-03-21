// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Stores
// DemoshopPaths.cs
// romak_000, 2015-03-21 2:11

using System.Configuration;
using Crocodev.Common.SmartConfiguration;

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