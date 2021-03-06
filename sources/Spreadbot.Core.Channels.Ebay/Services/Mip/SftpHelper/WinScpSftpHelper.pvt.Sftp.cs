﻿// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// WinScpSftpHelper.pvt.Sftp.cs

using System;
using System.IO;
using Krokodev.Common.Extensions;
using Spreadbot.Core.Channels.Ebay.Configuration.Settings;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Results;
using Spreadbot.Sdk.Common.Operations.Responses;
using WinSCP;

namespace Spreadbot.Core.Channels.Ebay.Services.Mip.SftpHelper
{
    public partial class WinScpSftpHelper
    {
        // --------------------------------------------------------[]
        private static Response< MipTestConnectionResult > DoTestConnection( string password )
        {
            try {
                var sessionOptions = SessionOptions( password );
                using( var session = new Session() ) {
                    session.Open( sessionOptions );
                }
            }
            catch( Exception exception ) {
                return new Response< MipTestConnectionResult >( exception );
            }
            return new Response< MipTestConnectionResult > {
                Result = new MipTestConnectionResult { Value = true }
            };
        }

        // --------------------------------------------------------[]
        private static Response< MipSftpSendFilesResult > DoSendFiles( string localFiles, string remoteFiles )
        {
            try {
                SftpUploadFiles( localFiles, remoteFiles );
            }
            catch( Exception exception ) {
                return new Response< MipSftpSendFilesResult >( exception ) {
                    Details = "localFiles: [{0}]".SafeFormat( localFiles )
                };
            }
            return new Response< MipSftpSendFilesResult > {
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
                HostName = EbaySettings.HostName,
                PortNumber = EbaySettings.PortNumber,
                UserName = EbaySettings.UserName,
                Password = password ?? EbaySettings.Password
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
        private static Response< MipFindRemoteFileResult > _FindRemoteFile( string filePrefix, string remoteDir )
        {
            var files = GetRemoteDirFiles( remoteDir );
            foreach( RemoteFileInfo fileInfo in files ) {
                if( fileInfo.Name.Contains( filePrefix ) ) {
                    return new Response< MipFindRemoteFileResult > {
                        Result =
                            new MipFindRemoteFileResult { RemoteDir = remoteDir, RemoteFileName = fileInfo.Name }
                    };
                }
            }
            return new Response< MipFindRemoteFileResult > {
                IsSuccessful = false,
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
        private Response< MipFindRemoteFileResult > _FindRemoteFile( string filePrefix, string[] remoteDirs )
        {
            foreach( var remoteDir in remoteDirs ) {
                var response = FindRemoteFile( filePrefix, remoteDir );
                if( response.IsSuccessful ) {
                    return response;
                }
            }
            return new Response< MipFindRemoteFileResult > {
                IsSuccessful = false,
                Details = string.Format( "Remote file [{0}] not found in [{1}]",
                    filePrefix,
                    remoteDirs.FoldToStringBy( s => s ) )
            };
        }
    }
}