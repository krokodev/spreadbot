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
            public static MipResponse<ZipFeedResult> ZipFeed(string feed, string reqId)
            {
                string zipFileName;
                try
                {
                    zipFileName = LocalZippedFeedFile(feed, reqId);
                    ZipFiles(
                        LocalFeedFolder(feed),
                        zipFileName
                        );
                }
                catch (Exception exception)
                {
                    return new MipResponse<ZipFeedResult>(false, MipStatusCode.ZipFeedFail, exception);
                }
                return new MipResponse<ZipFeedResult>(true, MipStatusCode.ZipFeedSuccess, new ZipFeedResult(zipFileName));
            }

            public static MipResponse<ZipFeedResult> ZipFeed(Feed feed, Request.Identifier reqId)
            {
                return ZipFeed(feed.Name, reqId.Value.ToString());
            }

            // ===================================================================================== []
            // ZipFeed
            public static string ZippedFeedFileName(string feed, string reqId)
            {
                return LocalZippedFeedFile(feed, reqId);
            }

            // ===================================================================================== []
            // ZipFiles
            private static void ZipFiles(string sourceDir, string zipFileName)
            {
                File.Delete(zipFileName);
                ZipFile.CreateFromDirectory(sourceDir, zipFileName, CompressionLevel.Optimal, false);
            }
        }
    }
}