using System;
using WinSCP;

namespace Spreadbot.Core.Mip
{
    public class MipConnector
    {
        // >> Now: MipConnector
        // ===================================================================================== []
        // TestConnection
        public MipResponse TestConnection(string password = null)
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
        public MipResponse UploadZippedFeed(MipFeed feed)
        {
            try
            {
                var sessionOptions = CreateSessionOptions();

                var transferOptions = new TransferOptions
                {
                    TransferMode = TransferMode.Binary
                };

                using (var session = new Session())
                {
                    session.Open(sessionOptions);
                    // Now: MipConnector.UploadZippedFeed
                    // Todo: Use config for paths
                    var transferResult = session.PutFiles(
                        @"p:\Projects\spreadbot\Research\mipsftp\data\store\zip\product.1324.zip",
                        "store/product/product.1324.zip",
                        false,
                        transferOptions
                        );
                    transferResult.Check();
                }
            }
            catch (Exception e)
            {
                return new MipResponse(MipStatusCode.Error)
                {
                    StatusDescription = e.Message
                };
            }
            return new MipResponse(MipStatusCode.FeedUploaded);
        }

        // ===================================================================================== []
        // CreateSessionOptions
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
    }
}