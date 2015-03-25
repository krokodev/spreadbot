// Spreadbot (c) 2015 Crocodev
// Spreadbot.Tests.Core
// MipConnectorTestInitializer.cs
// romak_000, 2015-03-25 13:08

using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoreLinq;
using Spreadbot.Core.Channels.Ebay.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Request;
using Spreadbot.Core.Channels.Ebay.Mip.Settings;
using Spreadbot.Sdk.Common.Crocodev.Common;

namespace Spreadbot.Tests.Core.Channels.Ebay.Mip
{
    [TestClass]
    public class MipConnectorTestInitializer
    {
        public const string ProductItemId = "321693290987";

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
        public static IEnumerable< string > TestRequestIds( MipFeedType feed, MipRequestStatus status )
        {
            return Enumerable.Range( 1, 10 )
                .Where( i => FileExists( feed, status, i ) )
                .Select( i => string.Format( "{0}-{1:000}", status, i ).ToLower() );
        }

        // --------------------------------------------------------[]
        private static bool FileExists( MipFeedType feed, MipRequestStatus status, int i )
        {
            return File.Exists(
                MipSettings.LocalBasePath
                    + @"ini\"
                    + string.Format( @"inbox\{0}.{1}-{2:000}.xml", feed, status, i ).ToLower()
                );
        }

        // --------------------------------------------------------[]
        private static void AddFeedStatusSamples( ICollection< string > files )
        {
            EnumUtil.GetValues< MipFeedType >().ForEach( feed => {
                EnumUtil.GetValues< MipRequestStatus >().ForEach( status => {
                    Enumerable.Range( 1, 10 )
                        .Where( i => FileExists( feed, status, i ) )
                        .Select( i => string.Format( @"inbox\{0}.{1}-{2:000}.xml", feed, status, i ).ToLower() )
                        .ForEach( files.Add );
                } );
            } );
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
    }
}