// Spreadbot (c) 2015 Crocodev
// Spreadbot.Tests.Core
// MipConnector.ini.cs
// romak_000, 2015-03-24 11:27

using System.Collections.Generic;
using System.IO;
using Crocodev.Common.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Spreadbot.Core.Channels.Ebay.Mip.Settings;

namespace Spreadbot.Tests.Core.Channels.Ebay.Mip
{
    [TestClass]
    public class MipConnectorTestInitializer
    {
        public const string ProductSuccessRequestId = "0000-product-success-0000";
        public const string ProductItemId = "321693290987";

        // Code: MipConnectorTestInitializer
        public static void PrepareTestFiles()
        {
            var basePath = MipSettings.BasePath;
            var files = new List< string > {
                @"zip\product.00000000-0000-0000-0000-000000000000.zip",
                @"src\availability\Availability_Single_SKU_One_Locale.xml",
                @"src\product\Product_Single_SKU_One_Locale.xml",
                @"src\distribution\Distribution_Single_SKU_One_Locale.xml",
                @"inbox\product.{0}.xml".SafeFormat(ProductSuccessRequestId),
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