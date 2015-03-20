﻿// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// MipConnector.ZipHelper.cs
// romak_000, 2015-03-20 13:56

using System;
using System.IO;
using System.IO.Compression;
using Spreadbot.Core.Channels.Ebay.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Response;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Results;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.StatusCode;

namespace Spreadbot.Core.Channels.Ebay.Mip.Connector
{
    public partial class MipConnector
    {
        public class ZipHelper
        {
            // ===================================================================================== []
            // ZipFeed
            public static MipResponse< MipZipFeedResult > ZipFeed( string feed, string reqId )
            {
                string zipFileName;
                try {
                    zipFileName = LocalZippedFeedFile( feed, reqId );
                    ZipFolderFiles(
                                   DoLocalFeedFolder( feed ),
                                   zipFileName
                        );
                }
                catch( Exception exception ) {
                    return new MipResponse< MipZipFeedResult >( false, MipStatusCode.ZipFeedFail, exception );
                }
                return new MipResponse< MipZipFeedResult >(
                    true,
                    MipStatusCode.ZipFeedSuccess,
                    new MipZipFeedResult( zipFileName ) );
            }

            // --------------------------------------------------------[]
            public static MipResponse< MipZipFeedResult > ZipFeed( MipFeed mipFeed, Guid reqId )
            {
                return ZipFeed( mipFeed.Name, reqId.ToString() );
            }

            // ===================================================================================== []
            // ZipFolderFiles
            private static void ZipFolderFiles( string sourceDir, string zipFileName )
            {
                File.Delete( zipFileName );
                ZipFile.CreateFromDirectory( sourceDir, zipFileName, CompressionLevel.Optimal, false );
            }
        }
    }
}