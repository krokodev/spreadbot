using System;
using System.IO;
using System.IO.Compression;

namespace Spreadbot.Core.Channel.Ebay.Mip
{
    public partial class Connector
    {
        public class ZipHelper
        {
            // ===================================================================================== []
            // ZipFeed
            public static Response<ZipFeedResult> ZipFeed(string feed, string reqId)
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
                    return new Response<ZipFeedResult>(false, StatusCode.ZipFeedFail, exception);
                }
                return new Response<ZipFeedResult>(true, StatusCode.ZipFeedSuccess, new ZipFeedResult(zipFileName));
            }

            public static Response<ZipFeedResult> ZipFeed(Feed feed, Request.Identifier reqId)
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