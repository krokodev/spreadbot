// ReSharper disable RedundantUsingDirective
using System;
using Crocodev.Common.Identifier;
using WinSCP;

namespace Spreadbot.Core.Mip
{
    public partial class MipConnector
    {
        // >> Now: MipConnector.Sftp
        public partial class SftpHelper
        {
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
}