// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Stores
// DemoshopPaths.cs

using System.Configuration;
using Krokodev.Common.SmartConfiguration;

namespace Spreadbot.Core.Stores.Demoshop.Configuration.Elements
{
    // Code: DemoshopPaths 
    public class DemoshopPaths : SmartConfigurationElement
    {
        [ConfigurationProperty( "AmazonTemplatesDir", IsRequired = false )]
        public string AmazonTemplatesDir
        {
            get { return ( string ) this[ GetPropertyName() ]; }
        }

        [ConfigurationProperty( "EbayTemplatesDir", IsRequired = false )]
        public string EbayTemplatesDir
        {
            get { return ( string ) this[ GetPropertyName() ]; }
        }

        [ConfigurationProperty( "DataFile", IsRequired = true )]
        public string DataFile
        {
            get { return ( string ) this[ GetPropertyName() ]; }
        }
    }
}