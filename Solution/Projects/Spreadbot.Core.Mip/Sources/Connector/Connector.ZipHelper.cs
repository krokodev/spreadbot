// ReSharper disable RedundantUsingDirective
using System;
using System.IO;
using System.IO.Compression;
using Crocodev.Common.Identifier;

namespace Spreadbot.Core.Mip
{
    public partial class Connector
    {
        public class ZipHelper
        {
            // ===================================================================================== []
            // ZipFeed
            public static Response ZipFeed(string feed, string reqId)
            {
                string zipFileName;
                try
                {
                    zipFileName = MakeLocalZippedFeedPath(feed, reqId);
                    ZipFiles(
                        MakeLocalFeedPath(feed),
                        zipFileName
                        );
                }
                catch (Exception e)
                {
                    return FailedResponse(StatusCode.ZipFeedFail, e);
                }
                return SuccessfulResponse(StatusCode.ZipFeedSuccess, zipFileName);
            }

            public static Response ZipFeed(Feed feed, Request.Identifier reqId)
            {
                return ZipFeed(feed.Name, reqId.Value.ToString());
            }

            // ===================================================================================== []
            // ZipFeed
            public static string ZippedFeedFileName(string feed, string reqId)
            {
                return MakeLocalZippedFeedPath(feed, reqId);
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