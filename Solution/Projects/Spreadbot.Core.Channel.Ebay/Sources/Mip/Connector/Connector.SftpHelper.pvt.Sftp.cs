using System;
using Crocodev.Common;
using WinSCP;

namespace Spreadbot.Core.Channel.Ebay.Mip
{
    public partial class Connector
    {
        public partial class SftpHelper
        {
            // ===================================================================================== []
            // DoTestConnection
            private static Response<BoolResult> DoTestConnection(string password)
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
                    return new Response<BoolResult>(false, StatusCode.TestConnectionFail, exception);
                }
                return new Response<BoolResult>(true, StatusCode.TestConnectionSuccess, new BoolResult(true));
            }

            // ===================================================================================== []
            // DoSendZippedFeed
            private static Response<SendFeedResult> DoSendZippedFeed(string feed, Request.Identifier reqId)
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
                    return new Response<SendFeedResult> (false, StatusCode.SendZippedFeedFail, exception);
                }
                return new Response<SendFeedResult>(true, StatusCode.SendZippedFeedSuccess, new SendFeedResult(reqId));
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
            private static Response<FindRemoteFileResult> FindRemoteFileNamePrefixInRemoteDir(string prefix,
                string remoteDir)
            {
                var files = GetRemoteDirFiles(remoteDir);
                foreach (RemoteFileInfo fileInfo in files)
                {
                    if (fileInfo.Name.Contains(prefix))
                    {
                        return new Response<FindRemoteFileResult>(
                            true,
                            StatusCode.FindRemoteFileSuccess,
                            new FindRemoteFileResult(remoteDir, fileInfo.Name)
                            );
                    }
                }
                return new Response<FindRemoteFileResult>(
                    false,
                    StatusCode.FindRemoteFileFail,
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