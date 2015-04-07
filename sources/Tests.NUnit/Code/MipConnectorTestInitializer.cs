// Spreadbot (c) 2015 Crocodev
// Tests.NUnit
// MipConnectorTestInitializer.cs
// Roman, 2015-04-07 12:25 PM

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Crocodev.Common.Extensions;
using MoreLinq;
using Spreadbot.Core.Channels.Ebay.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Request;
using Spreadbot.Core.Channels.Ebay.Mip.Settings;
using Spreadbot.Sdk.Common.Crocodev.Common;
using Spreadbot.Sdk.Common.Exceptions;

namespace Tests.NUnit.Code
{
    public class MipConnectorTestInitializer
    {
        public const string ProductItemId = "321693290987";
        public const string ItemRequestId = "item-present";

        // --------------------------------------------------------[]
        public static void PrepareTestFiles()
        {
            var files = new List< string > {
                @"zip\product.00000000-0000-0000-0000-000000000000.zip",
                @"src\availability\Availability_Single_SKU_One_Locale.xml",
                @"src\product\Product_Single_SKU_One_Locale.xml",
                @"src\distribution\Distribution_Single_SKU_One_Locale.xml",
                @"inbox\product.{0}.xml".SafeFormat( ItemRequestId ),
                @"inbox\distribution.{0}.xml".SafeFormat( ItemRequestId ),
            };

            AddFeedStatusSamples( files );
            CopyFromIniToStore( files );
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
        private static void CopyFromIniToStore( List< string > files )
        {
            files.ForEach(
                file => {
                    var iniFile = MipSettings.LocalBasePath + @"ini\" + file;
                    var storeFile = MipSettings.LocalBasePath + @"store\" + file;
                    var storeFolder = Path.GetDirectoryName( storeFile );

                    if( storeFolder == null ) {
                        throw new SpreadbotException( "Unknown folder for file [{0}]", storeFile );
                    }
                    if( !Directory.Exists( storeFolder ) ) {
                        Directory.CreateDirectory( storeFolder );
                    }
                    if( File.Exists( storeFile ) ) {
                        File.Delete( storeFile );
                    }
                    if( !File.Exists( iniFile ) ) {
                        Console.WriteLine( "File [{0}] not found", iniFile );
                    } else {
                        File.Copy( iniFile, storeFile );
                    }
                } );
        }
    }
}