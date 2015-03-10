using System;
using Crocodev.Common;
using WinSCP;

namespace Spreadbot.Core.Channel.Ebay.Mip
{
    public partial class MipConnector
    {
        public partial class SftpHelper
        {
            // ===================================================================================== []
            // DoTestConnection
            private static MipResponse<BoolResult> DoTestConnection(string password)
            {
                try
                {
                    var sessionOptions = SessionOptions(password);
                    using (var session = new Session())
                    {
                        session.Open(sessionOptions);
                    }
                }
                catch (Exception exception)
                {
                    return new MipResponse<BoolResult>(false, MipStatusCode.TestConnectionFail, exception);
                }
                return new MipResponse<BoolResult>(true, MipStatusCode.TestConnectionSuccess, new BoolResult(true));
            }

            // ===================================================================================== []
            // DoSendZippedFeed
            private static MipResponse<SendFeedResult> DoSendZippedFeed(string feed, Request.Identifier reqId)
            {
                try
                {
                    UploadFiles(
                        LocalZippedFeedFile(feed, reqId.ToString()),
                        RemoteFeedOutgoingZipFilePath(feed, reqId.ToString())
                        );
                }
                catch (Exception exception)
                {
                    return new MipResponse<SendFeedResult> (false, MipStatusCode.SendZippedFeedFail, exception);
                }
                return new MipResponse<SendFeedResult>(true, MipStatusCode.SendZippedFeedSuccess, new SendFeedResult(reqId));
            }

            // ===================================================================================== []
            // GetRemoteDirFiles
            private static RemoteFileInfoCollection GetRemoteDirFiles(string remoteDir)
            {
                try
                {
                    using (var session = new Session())
                    {
                        var sessionOptions = SessionOptions();
                        session.Open(sessionOptions);
                        return session.ListDirectory(remoteDir).Files;
                    }
                }
                catch (Exception exception)
                {
                    if (!exception.Message.Contains("Error listing directory"))
                    {
                        throw;
                    }
                }
                return new RemoteFileInfoCollection();
            }

            // ===================================================================================== []
            // FindFileNamePrefixInRemoteDir
            private static MipResponse<FindRemoteFileResult> FindRemoteFileNamePrefixInRemoteDir(string prefix,
                string remoteDir)
            {
                var files = GetRemoteDirFiles(remoteDir);
                foreach (RemoteFileInfo fileInfo in files)
                {
                    if (fileInfo.Name.Contains(prefix))
                    {
                        return new MipResponse<FindRemoteFileResult>(
                            true,
                            MipStatusCode.FindRemoteFileSuccess,
                            new FindRemoteFileResult(remoteDir, fileInfo.Name)
                            );
                    }
                }
                return new MipResponse<FindRemoteFileResult>(
                    false,
                    MipStatusCode.FindRemoteFileFail,
                    "Remote file [{0}] not found in [{1}]".SafeFormat(prefix, remoteDir)
                    );
            }

            // ===================================================================================== []
            // UploadFiles
            private static void UploadFiles(string localPath, string remotePath)
            {
                using (var session = new Session())
                {
                    var sessionOptions = SessionOptions();
                    var transferOptions = TransferOptions();

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
            // UploadFiles
            private static void DownloadFiles(string remotePath, string localPath)
            {
                using (var session = new Session())
                {
                    var sessionOptions = SessionOptions();
                    var transferOptions = TransferOptions();

                    session.Open(sessionOptions);

                    var transferResult = session.GetFiles(
                        remotePath,
                        localPath,
                        false,
                        transferOptions
                        );

                    transferResult.Check();
                }
            }

            // ===================================================================================== []
            // SessionOptions
            private static SessionOptions SessionOptions(string password = null)
            {
                return new SessionOptions
                {
                    Protocol = Protocol.Sftp,
                    GiveUpSecurityAndAcceptAnySshHostKey = true,
                    HostName = Settings.HostName,
                    PortNumber = Settings.PortNumber,
                    UserName = Settings.UserName,
                    Password = password ?? Settings.Password
                };
            }

            // ===================================================================================== []
            // TransferOptions
            private static TransferOptions TransferOptions()
            {
                return new TransferOptions
                {
                    TransferMode = TransferMode.Binary
                };
            }
        }
    }
}