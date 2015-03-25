// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// MipConnector.ZipHelper.cs
// romak_000, 2015-03-21 2:11

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
                    return new MipResponse< MipZipFeedResult >( false, MipOperationStatus.ZipFeedFailure, exception );
                }
                return new MipResponse< MipZipFeedResult >(
                    true,
                    MipOperationStatus.ZipFeedSuccess,
                    new MipZipFeedResult( zipFileName ) );
            }

            // --------------------------------------------------------[]
            public static MipResponse< MipZipFeedResult > ZipFeed( MipFeedHandler mipFeedHandler, string reqId )
            {
                return ZipFeed( mipFeedHandler.GetName(), reqId );
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