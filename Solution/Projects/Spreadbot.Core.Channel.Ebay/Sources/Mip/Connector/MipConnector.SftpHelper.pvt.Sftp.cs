using System;
using Crocodev.Common;
using Spreadbot.Core.Common;
using Spreadbot.Sdk.Common;
using WinSCP;

namespace Spreadbot.Core.Channel.Ebay.Mip
{
    public partial class MipConnector
    {
        public partial class SftpHelper
        {
            // ===================================================================================== []
            // DoTestConnection
            private static MipResponse<MipTestConnectionResult> DoTestConnection(string password)
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
                    return new MipResponse<MipTestConnectionResult>(false, MipStatusCode.TestConnectionFail, exception);
                }
                return new MipResponse<MipTestConnectionResult>(true, MipStatusCode.TestConnectionSuccess, new MipTestConnectionResult(true));
            }

            // ===================================================================================== []
            // DoSendZippedFeed
            private static MipResponse<MipSendZippedFeedFolderResult> DoSendZippedFeed(string feed, MipRequest.Identifier reqId)
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
                    return new MipResponse<MipSendZippedFeedFolderResult> (false, MipStatusCode.SendZippedFeedFail, exception);
                }
                return new MipResponse<MipSendZippedFeedFolderResult>(true, MipStatusCode.SendZippedFeedSuccess, new MipSendZippedFeedFolderResult(reqId));
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
            private static MipResponse<MipFindRemoteFileResult> FindRemoteFileNamePrefixInRemoteDir(string prefix,
                string remoteDir)
            {
                var files = GetRemoteDirFiles(remoteDir);
                foreach (RemoteFileInfo fileInfo in files)
                {
                    if (fileInfo.Name.Contains(prefix))
                    {
                        return new MipResponse<MipFindRemoteFileResult>(
                            true,
                            MipStatusCode.FindRemoteFileSuccess,
                            new MipFindRemoteFileResult(remoteDir, fileInfo.Name)
                            );
                    }
                }
                return new MipResponse<MipFindRemoteFileResult>(
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
                    HostName = MipSettings.HostName,
                    PortNumber = MipSettings.PortNumber,
                    UserName = MipSettings.UserName,
                    Password = password ?? MipSettings.Password
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