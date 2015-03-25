// Spreadbot (c) 2015 Crocodev
// Spreadbot.Tests.Core
// MipConnectorTestInitializer.cs
// romak_000, 2015-03-25 12:27

using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoreLinq;
using Spreadbot.Core.Channels.Ebay.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Mip.Settings;
using Spreadbot.Sdk.Common.Crocodev.Common;
using Spreadbot.Sdk.Common.Operations.Tasks;

namespace Spreadbot.Tests.Core.Channels.Ebay.Mip
{
    [TestClass]
    public class MipConnectorTestInitializer
    {
        public const string ProductItemId = "321693290987";
        public const string ProductSuccessRequestId = "0000-product-success-0000";
        public const string ProductFailRequestId = "0000-product-fail-0000";

        // Code: MipConnectorTestInitializer
        // --------------------------------------------------------[]
        public static void PrepareTestFiles()
        {
            var files = new List< string > {
                @"zip\product.00000000-0000-0000-0000-000000000000.zip",
                @"src\availability\Availability_Single_SKU_One_Locale.xml",
                @"src\product\Product_Single_SKU_One_Locale.xml",
                @"src\distribution\Distribution_Single_SKU_One_Locale.xml",
            };

            AddFeedStatusSamples( files );
            CopyFronIniToStore( files );
        }

        // --------------------------------------------------------[]
        private static void CopyFronIniToStore( List< string > files )
        {
            files.ForEach(
                file => {
                    var iniFile = MipSettings.LocalBasePath + @"ini\" + file;
                    var storeFile = MipSettings.LocalBasePath + @"store\" + file;
                    File.Delete( storeFile );
                    File.Copy( iniFile, storeFile );
                } );
        }

        // --------------------------------------------------------[]
        private static void AddFeedStatusSamples( ICollection< string > files )
        {
            EnumUtil.GetStringValues< MipFeedType >().ForEach( feed => {
                EnumUtil.GetStringValues< TaskStatus >().ForEach( ts => {
                    Enumerable.Range( 1, 10 )
                        .Select( i => string.Format( @"inbox\{0}.{1}-{2:000}.xml", feed, ts, i ).ToLower() )
                        .Where( f => File.Exists( MipSettings.LocalBasePath + @"ini\" + f ) )
                        .ForEach( files.Add );
                } );
            } );
        }
    }
}