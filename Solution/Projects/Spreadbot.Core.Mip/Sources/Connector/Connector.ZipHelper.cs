using System;
using System.IO;
using System.IO.Compression;

namespace Spreadbot.Core.Mip
{
    public partial class Connector
    {
        public class ZipHelper
        {
            // ===================================================================================== []
            // ZipFeed
            public static Response<ZippingFeedResult> ZipFeed(string feed, string reqId)
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
                catch (Exception e)
                {
                    return Response<ZippingFeedResult>.NewFail(StatusCode.ZipFeedFail, e);
                }
                return Response<ZippingFeedResult>.NewSuccess(StatusCode.ZipFeedSuccess, new ZippingFeedResult(zipFileName));
            }

            public static Response<ZippingFeedResult> ZipFeed(Feed feed, Request.Identifier reqId)
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