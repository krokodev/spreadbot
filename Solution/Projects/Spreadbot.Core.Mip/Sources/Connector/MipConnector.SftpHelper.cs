// ReSharper disable RedundantUsingDirective
using System;
using Crocodev.Common.Identifier;
using WinSCP;

namespace Spreadbot.Core.Mip
{
    public partial class MipConnector
    {
        // >> Now: MipConnector.SftpHelper
        public partial class SftpHelper
        {
            // ===================================================================================== []
            // TestConnection
            public static MipResponse TestConnection(string password = null)
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
                    return new MipResponse(MipStatusCode.Error)
                    {
                        StatusDescription = e.Message
                    };
                }
                return new MipResponse(MipStatusCode.ConnectionOk);
            }
            
            // ===================================================================================== []
            // UploadFeed
            public static MipResponse SendZippedFeed(MipFeed feed, MipRequest.Identifier reqId)
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
                    return new MipResponse(MipStatusCode.Error, e.Message);
                }
                return new MipResponse(MipStatusCode.FeedUploaded);
            }
        }
    }
}