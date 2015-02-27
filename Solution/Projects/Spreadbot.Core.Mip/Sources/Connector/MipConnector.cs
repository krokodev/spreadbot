using System;
using Crocodev.Common.Identifier;
using WinSCP;

namespace Spreadbot.Core.Mip
{
    public class MipConnector
    {
        // >> Now: MipConnector
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

        // ===================================================================================== []
        // Session
        private static void PutFiles(string localPath, string remotePath)
        {
            using (var session = new Session())
            {
                var sessionOptions = CreateSessionOptions();
                var transferOptions = CreateTransferOptions();

                session.Open(sessionOptions);

                var transferResult = session.PutFiles(
                    localPath,
                    remotePath,
                    false,
                    transferOptions
                    );

                transferResult.Check();
            }
        }

        // ===================================================================================== []
        // Paths
        private static string MakeLocalZippedFeedPath(MipFeed feed, MipRequest.Identifier reqId)
        {
            return string.Format("{0}{1}.{2}.zip",
                MipSettings.ZippedFeedsPath,
                feed.Name,
                reqId
                );
        }

        private static string MakeRemoteFeedInboxPath(MipFeed feed, MipRequest.Identifier reqId)
        {
            return string.Format("{0}{1}/{1}.{2}.zip",
                MipSettings.RemoteBasePath,
                feed.Name,
                reqId
                );
        }


        // ===================================================================================== []
        // Options
        private static SessionOptions CreateSessionOptions(string password = null)
        {
            return new SessionOptions
            {
                Protocol = Protocol.Sftp,
                GiveUpSecurityAndAcceptAnySshHostKey = true,
                HostName = MipSettings.HostName,
                PortNumber = MipSettings.PortNumber,
                UserName = MipSettings.UserName,
                Password = password ?? MipSettings.Password
            };
        }

        private static TransferOptions CreateTransferOptions()
        {
            return new TransferOptions
            {
                TransferMode = TransferMode.Binary
            };
        }
    }
}