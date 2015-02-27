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
                try
                {
                    ZipFiles(
                        MakeLocalFeedPath(feed),
                        MakeLocalZippedFeedPath(feed, reqId)
                        );
                }
                catch (Exception e)
                {
                    return new Response(false, StatusCode.ZipFeedFail, e.Message);
                }
                return new Response(true, StatusCode.ZipFeedSuccess);
            }

            public static Response ZipFeed(Feed feed, Request.Identifier reqId)
            {
                return ZipFeed(feed.Name, reqId.Value.ToString());
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