using System;
using Crocodev.Common;
using WinSCP;

namespace Spreadbot.Core.Mip
{
    public partial class Connector
    {
        public partial class SftpHelper
        {
            // ===================================================================================== []
            // DoTestConnection
            private static Response DoTestConnection(string password)
            {
                try
                {
                    var sessionOptions = SessionOptions(password);
                    using (var session = new Session())
                    {
                        session.Open(sessionOptions);
                    }
                }
                catch (Exception e)
                {
                    return Response.ResponseFail(StatusCode.TestConnectionFail, e);
                }
                return Response.ResponseSuccess(StatusCode.TestConnectionSuccess);
            }

            // ===================================================================================== []
            // DoSendZippedFeed
            private static Response DoSendZippedFeed(string feed, string reqId)
            {
                string remoteFileName;
                try
                {
                    remoteFileName = RemoteFeedOutgoingZipFilePath(feed, reqId);
                    PutFiles(
                        LocalZippedFeedFile(feed, reqId),
                        remoteFileName
                        );
                }
                catch (Exception e)
                {
                    return Response.ResponseFail(StatusCode.SendZippedFeedFail, e);
                }
                return Response.ResponseSuccess(StatusCode.SendZippedFeedSuccess, remoteFileName);
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
                catch (Exception e)
                {
                    if (!e.Message.Contains("Error listing directory"))
                    {
                        throw;
                    }
                }
                return new RemoteFileInfoCollection();
            }

            // ===================================================================================== []
            // FindFileNamePrefixInRemoteDir
            private static Response FindRemoteFileNamePrefixInRemoteDir(string prefix, string remoteDir)
            {
                var files = GetRemoteDirFiles(remoteDir);
                foreach (RemoteFileInfo fileInfo in files)
                {
                    if (fileInfo.Name.Contains(prefix))
                    {
                        return Response.ResponseSuccess(StatusCode.FindRemoteFileSuccess, new FindRemoteFileResult(remoteDir, fileInfo.Name));
                    }
                }
                return Response.ResponseFail(StatusCode.FindRemoteFileFail,
                    "Remote file [{0}] not found in [{1}]".SafeFormat(prefix, remoteDir));
            }

            // ===================================================================================== []
            // PutFiles
            private static void PutFiles(string localPath, string remotePath)
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
