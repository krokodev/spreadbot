// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// MipConnector.SftpHelper.pvt.Sftp.cs
// Roman, 2015-04-03 8:16 PM

using System;
using Crocodev.Common.Extensions;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Response;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Results;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.StatusCode;
using Spreadbot.Core.Channels.Ebay.Mip.Settings;
using WinSCP;

namespace Spreadbot.Core.Channels.Ebay.Mip.Connector
{
    public partial class MipConnector
    {
        public partial class SftpHelper
        {
            // --------------------------------------------------------[]
            private static MipResponse< MipTestConnectionResult > DoTestConnection( string password )
            {
                try {
                    var sessionOptions = SessionOptions( password );
                    using( var session = new Session() ) {
                        session.Open( sessionOptions );
                    }
                }
                catch( Exception exception ) {
                    return new MipResponse< MipTestConnectionResult >( exception ) {
                        StatusCode = MipOperationStatus.TestConnectionFailure
                    };
                }
                return new MipResponse< MipTestConnectionResult > {
                    StatusCode = MipOperationStatus.TestConnectionSuccess,
                    Result = new MipTestConnectionResult { Value = true }
                };
            }

            // --------------------------------------------------------[]
            private static MipResponse< MipSftpSendFilesResult > DoSendFiles( string localFiles, string remoteFiles )
            {
                try {
                    SftpUploadFiles( localFiles, remoteFiles );
                }
                catch( Exception exception ) {
                    return new MipResponse< MipSftpSendFilesResult >( exception ) {
                        StatusCode = MipOperationStatus.SftpSendFilesFailure,
                        Details = "localFiles: [{0}]".SafeFormat( localFiles )
                    };
                }
                return new MipResponse< MipSftpSendFilesResult > {
                    StatusCode = MipOperationStatus.SftpSendFilesSuccess,
                    Result = new MipSftpSendFilesResult { LocalFiles = localFiles, RemoteFiles = remoteFiles }
                };
            }

            // --------------------------------------------------------[]
            private static RemoteFileInfoCollection GetRemoteDirFiles( string remoteDir )
            {
                try {
                    using( var session = new Session() ) {
                        var sessionOptions = SessionOptions();
                        session.Open( sessionOptions );
                        return session.ListDirectory( remoteDir ).Files;
                    }
                }
                catch( Exception exception ) {
                    if( !exception.Message.Contains( "Error listing directory" ) ) {
                        throw;
                    }
                }
                return new RemoteFileInfoCollection();
            }

            // --------------------------------------------------------[]
            private static void SftpUploadFiles( string localPath, string remotePath )
            {
                using( var session = new Session() ) {
                    var sessionOptions = SessionOptions();
                    var transferOptions = TransferOptions();

                    session.Open( sessionOptions );

                    var transferResult = session.PutFiles(
                        localPath,
                        remotePath,
                        false,
                        transferOptions
                        );

                    transferResult.Check();
                }
            }

            // --------------------------------------------------------[]
            private static void DoDownloadFiles( string remotePath, string localPath )
            {
                using( var session = new Session() ) {
                    var sessionOptions = SessionOptions();
                    var transferOptions = TransferOptions();

                    session.Open( sessionOptions );

                    var transferResult = session.GetFiles(
                        remotePath,
                        localPath,
                        false,
                        transferOptions
                        );

                    transferResult.Check();
                }
            }

            // --------------------------------------------------------[]
            private static SessionOptions SessionOptions( string password = null )
            {
                return new SessionOptions {
                    Protocol = Protocol.Sftp,
                    GiveUpSecurityAndAcceptAnySshHostKey = true,
                    HostName = MipSettings.HostName,
                    PortNumber = MipSettings.PortNumber,
                    UserName = MipSettings.UserName,
                    Password = password ?? MipSettings.Password
                };
            }

            // --------------------------------------------------------[]
            private static TransferOptions TransferOptions()
            {
                return new TransferOptions {
                    TransferMode = TransferMode.Binary
                };
            }
        }
    }
}