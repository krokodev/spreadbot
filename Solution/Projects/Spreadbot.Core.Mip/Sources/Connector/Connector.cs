// ReSharper disable RedundantUsingDirective
using System;
using Crocodev.Common.Identifier;
using WinSCP;

namespace Spreadbot.Core.Mip
{
    public partial class Connector
    {
        // Now: MipConnector
        public static Response SendFeed(Feed feed)
        {
            var reqId = Request.GenerateRequestId();
            try
            {
                //ZipHelper.CreateArchive(feed, reqId);
                SftpHelper.SendZippedFeed(feed, reqId).Check();
            }
            catch (Exception e)
            {
                return new Response(
                    false,
                    StatusCode.SendFeedError,
                    string.Format("Feed:[{0}.{1}]", feed, reqId)
                    );

            }
            return new Response(
                true,
                StatusCode.SendFeedOk,
                string.Format("Feed:[{0}.{1}] zipped and sent", feed, reqId)
                );
        }
    }
}