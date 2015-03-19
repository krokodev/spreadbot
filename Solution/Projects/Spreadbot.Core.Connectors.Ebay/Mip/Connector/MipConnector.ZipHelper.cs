// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Connectors.Ebay
// MipConnector.ZipHelper.cs
// romak_000, 2015-03-19 15:38

using System;
using System.IO;
using System.IO.Compression;
using Spreadbot.Core.Channel.Ebay.Mip.Feed;
using Spreadbot.Core.Channel.Ebay.Mip.Operations.Response;
using Spreadbot.Core.Channel.Ebay.Mip.Operations.Results;
using Spreadbot.Core.Channel.Ebay.Mip.Operations.StatusCode;

namespace Spreadbot.Core.Channel.Ebay.Mip.Connector
{
    public partial class MipConnector
    {
        public class ZipHelper
        {
            // ===================================================================================== []
            // ZipFeed
            public static MipResponse<MipZipFeedResult> ZipFeed(string feed, string reqId)
            {
                string zipFileName;
                try
                {
                    zipFileName = LocalZippedFeedFile(feed, reqId);
                    ZipFolderFiles(
                        DoLocalFeedFolder(feed),
                        zipFileName
                        );
                }
                catch (Exception exception)
                {
                    return new MipResponse<MipZipFeedResult>(false, MipStatusCode.ZipFeedFail, exception);
                }
                return new MipResponse<MipZipFeedResult>(true, MipStatusCode.ZipFeedSuccess,
                    new MipZipFeedResult(zipFileName));
            }

            // --------------------------------------------------------[]
            public static MipResponse<MipZipFeedResult> ZipFeed(MipFeed mipFeed, Guid reqId)
            {
                return ZipFeed(mipFeed.Name, reqId.ToString());
            }

            // ===================================================================================== []
            // ZipFolderFiles
            private static void ZipFolderFiles(string sourceDir, string zipFileName)
            {
                File.Delete(zipFileName);
                ZipFile.CreateFromDirectory(sourceDir, zipFileName, CompressionLevel.Optimal, false);
            }
        }
    }
}