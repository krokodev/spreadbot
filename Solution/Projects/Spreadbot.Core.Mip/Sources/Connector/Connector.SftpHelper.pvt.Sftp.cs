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
                        return ResponseSuccess(StatusCode.FindRemoteFileSuccess, fileInfo.Name);
                    }
                }
                return ResponseFail(StatusCode.FindRemoteFileFail,
                    "Remote file [{0}] not found in [{1}]".SafeFormat(prefix, remoteDir));
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
