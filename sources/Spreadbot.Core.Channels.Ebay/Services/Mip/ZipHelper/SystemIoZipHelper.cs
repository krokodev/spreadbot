// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// SystemIoZipHelper.cs

using System;
using System.IO;
using System.IO.Compression;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Connector;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Operations.Results;
using Spreadbot.Sdk.Common.Operations.Responses;

namespace Spreadbot.Core.Channels.Ebay.Services.Mip.ZipHelper
{
    public class SystemIoZipHelper : IZipHelper
    {
        // --------------------------------------------------------[]
        public Response< MipZipFeedResult > ZipFeed( MipFeedDescriptor mipFeedDescriptor, string reqId )
        {
            string zipFileName;
            try {
                zipFileName = MipConnector.LocalZippedFeedFile( mipFeedDescriptor.GetName(), reqId );
                ZipFolderFiles(
                    MipConnector.LocalFeedFolder( mipFeedDescriptor ),
                    zipFileName
                    );
            }
            catch( Exception exception ) {
                return new Response< MipZipFeedResult >( exception );
            }
            return new Response< MipZipFeedResult > {
                Result = new MipZipFeedResult { ZipFileName = zipFileName }
            };
        }

        // --------------------------------------------------------[]
        private void ZipFolderFiles( string sourceDir, string zipFileName )
        {
            File.Delete( zipFileName );
            ZipFile.CreateFromDirectory( sourceDir, zipFileName, CompressionLevel.Optimal, false );
        }
    }
}