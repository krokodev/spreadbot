// Spreadbot (c) 2015 Crocodev
// Spreadbot.Tests.Core
// MipTestInitializer.cs
// romak_000, 2015-03-20 15:33

using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spreadbot.Core.Channels.Ebay.Mip.Settings;

namespace Spreadbot.Tests.Core.Channels.Ebay.Mip
{
    [TestClass]
    public class MipTestInitializer
    {
        // ===================================================================================== []
        public static void PrepareTestFiles()
        {
            var basePath = MipSettings.BasePath;
            var files = new List< string > {
                @"zip\product.00000000-0000-0000-0000-000000000000.zip",
                @"src\availability\Availability_Single_SKU_One_Locale.xml",
                @"src\product\Product_Single_SKU_One_Locale.xml",
                @"src\distribution\Distribution_Single_SKU_One_Locale.xml"
            };

            files.ForEach(
                file => {
                    var iniFile = basePath + @"ini\" + file;
                    var storeFile = basePath + @"store\" + file;
                    File.Delete( storeFile );
                    File.Copy( iniFile, storeFile );
                } );
        }
    }
}