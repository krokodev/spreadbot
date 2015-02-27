// ReSharper disable RedundantUsingDirective

using System;
using System.Diagnostics;
using Crocodev.Common.Identifier;
using WinSCP;

namespace Spreadbot.Core.Mip
{
    public partial class Connector
    {
        // >> Now: MipConnector.SftpHelper
        public partial class SftpHelper
        {
            // ===================================================================================== []
            // TestConnection
            public static Response TestConnection(string password = null)
            {
                try
                {
                    var sessionOptions = CreateSessionOptions(password);
                    using (var session = new Session())
                    {
                        session.Open(sessionOptions);
                    }
                }
                catch (Exception e)
                {
                    return new Response(false, StatusCode.TestConnectionError, e.Message);
                }
                return new Response(true, StatusCode.TestConnectionOk);
            }

            // ===================================================================================== []
            // UploadFeed
            public static Response SendZippedFeed(string feed, string reqId)
            {
                try
                {
                    PutFiles(
                        MakeLocalZippedFeedPath(feed, reqId),
                        MakeRemoteFeedInboxPath(feed, reqId)
                        );
                }
                catch (Exception e)
                {
                    return new Response(false, StatusCode.SendZippedFeedError, e.Message);
                }
                return new Response(false, StatusCode.SendZippedFeedOk);
            }

            public static Response SendZippedFeed(Feed feed, Request.Identifier reqId)
            {
                return SendZippedFeed(feed.Name, reqId.Value.ToString());
            }
        }
    }
}