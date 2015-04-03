// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// MipConnector.ZipHelper.cs
// Roman, 2015-04-03 8:16 PM

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
                    return new MipResponse< MipZipFeedResult >( exception ) {
                        StatusCode = MipOperationStatus.ZipFeedFailure
                    };
                }
                return new MipResponse< MipZipFeedResult > {
                    StatusCode = MipOperationStatus.ZipFeedSuccess,
                    Result = new MipZipFeedResult { ZipFileName = zipFileName }
                };
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