using System;
using System.IO;
using System.IO.Compression;

namespace Spreadbot.Core.Channel.Ebay.Mip
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
                return new MipResponse<MipZipFeedResult>(true, MipStatusCode.ZipFeedSuccess, new MipZipFeedResult(zipFileName));
            }

            // --------------------------------------------------------[]
            public static MipResponse<MipZipFeedResult> ZipFeed(MipFeed mipFeed, MipRequest.Identifier reqId)
            {
                return ZipFeed(mipFeed.Name, reqId.Value.ToString());
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