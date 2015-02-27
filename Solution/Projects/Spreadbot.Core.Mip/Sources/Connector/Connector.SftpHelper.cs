// ReSharper disable RedundantUsingDirective
using System;
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
                    return new Response(StatusCode.Error)
                    {
                        StatusDescription = e.Message
                    };
                }
                return new Response(StatusCode.ConnectionOk);
            }
            
            // ===================================================================================== []
            // UploadFeed
            public static Response SendZippedFeed(Feed feed, Request.Identifier reqId)
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
                    return new Response(StatusCode.Error, e.Message);
                }
                return new Response(StatusCode.FeedUploaded);
            }
        }
    }
}