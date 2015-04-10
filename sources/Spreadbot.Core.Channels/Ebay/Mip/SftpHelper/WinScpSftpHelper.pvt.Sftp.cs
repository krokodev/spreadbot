// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// WinScpSftpHelper.pvt.Sftp.cs
// Roman, 2015-04-10 1:29 PM

using System;
using System.IO;
using Crocodev.Common.Extensions;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Response;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Results;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.StatusCode;
using Spreadbot.Core.Channels.Ebay.Mip.Settings;
using WinSCP;

namespace Spreadbot.Core.Channels.Ebay.Mip.SftpHelper
{
    public partial class WinScpSftpHelper
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
        private static void _DownloadFiles( string remotePath, string localPath )
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

        // --------------------------------------------------------[]
        private static MipResponse< MipFindRemoteFileResult > _FindRemoteFile( string filePrefix, string remoteDir )
        {
            var files = GetRemoteDirFiles( remoteDir );
            foreach( RemoteFileInfo fileInfo in files ) {
                if( fileInfo.Name.Contains( filePrefix ) ) {
                    return new MipResponse< MipFindRemoteFileResult > {
                        StatusCode = MipOperationStatus.FindRemoteFileSuccess,
                        Result =
                            new MipFindRemoteFileResult { RemoteDir = remoteDir, RemoteFileName = fileInfo.Name }
                    };
                }
            }
            return new MipResponse< MipFindRemoteFileResult > {
                IsSuccess = false,
                StatusCode = MipOperationStatus.FindRemoteFileFailure,
                Details = string.Format( "Remote file [{0}] not found in [{1}]", filePrefix, remoteDir )
            };
        }

        // --------------------------------------------------------[]
        private static string _GetRemoteFileContent( string remoteFolder, string fileName, string localFolder )
        {
            var remotePath = string.Format( @"{0}/{1}", remoteFolder, fileName );
            var localPath = string.Format( @"{0}\{1}", localFolder, fileName );

            _DownloadFiles( remotePath, localPath );

            return File.ReadAllText( string.Format( @"{0}\{1}", localFolder, fileName ) );
        }

        // --------------------------------------------------------[]
        private MipResponse< MipFindRemoteFileResult > _FindRemoteFile( string filePrefix, string[] remoteDirs )
        {
            foreach( var remoteDir in remoteDirs ) {
                var response = FindRemoteFile( filePrefix, remoteDir );
                if( response.StatusCode == MipOperationStatus.FindRemoteFileSuccess ) {
                    return response;
                }
            }
            return new MipResponse< MipFindRemoteFileResult > {
                IsSuccess = false,
                StatusCode = MipOperationStatus.FindRemoteFileFailure,
                Details = string.Format( "Remote file [{0}] not found in [{1}]",
                    filePrefix,
                    remoteDirs.FoldToStringBy( s => s ) )
            };
        }
    }
}