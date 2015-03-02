// ReSharper disable RedundantUsingDirective

using System;
using System.Diagnostics;
using Crocodev.Common.Identifier;
using WinSCP;

namespace Spreadbot.Core.Mip
{
    public partial class Connector
    {
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
                    return FailedResponse(StatusCode.TestConnectionFail, e);
                }
                return SuccessfulResponse(StatusCode.TestConnectionSuccess);
            }

            // ===================================================================================== []
            // UploadFeed
            public static Response SendZippedFeed(string feed, string reqId)
            {
                string remoteFileName;
                try
                {
                    remoteFileName = MakeRemoteFeedOutboxPath(feed, reqId);
                    PutFiles(
                        MakeLocalZippedFeedPath(feed, reqId),
                        remoteFileName
                        );
                }
                catch (Exception e)
                {
                    return FailedResponse(StatusCode.SendZippedFeedFail, e);
                }
                return SuccessfulResponse(StatusCode.SendZippedFeedSuccess, remoteFileName);
            }

            public static Response SendZippedFeed(Feed feed, Request.Identifier reqId)
            {
                return SendZippedFeed(feed.Name, reqId.Value.ToString());
            }

            // ===================================================================================== []
            // Find remote files
            public static Response FindRequestRemoteFileNameInInprocess(Request request)
            {
                throw new NotImplementedException();
            }

            public static Response FindRequestRemoteFileNameInOutput(Request request)
            {
                throw new NotImplementedException();
            }

            public static Response FindRequestRemoteFileNameAnywhere(Request request)
            {
                throw new NotImplementedException();
            }
        }
    }
}