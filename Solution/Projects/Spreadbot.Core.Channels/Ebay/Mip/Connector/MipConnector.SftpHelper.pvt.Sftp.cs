// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// MipConnector.SftpHelper.pvt.Sftp.cs
// romak_000, 2015-03-21 2:11

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
            // ===================================================================================== []
            // DoTestConnection
            private static MipResponse< MipTestConnectionResult > DoTestConnection( string password )
            {
                try {
                    var sessionOptions = SessionOptions( password );
                    using( var session = new Session() ) {
                        session.Open( sessionOptions );
                    }
                }
                catch( Exception exception ) {
                    return new MipResponse< MipTestConnectionResult >(
                        false,
                        MipOperationStatus.TestConnectionFailure,
                        exception );
                }
                return new MipResponse< MipTestConnectionResult >(
                    true,
                    MipOperationStatus.TestConnectionSuccess,
                    new MipTestConnectionResult( true ) );
            }

            // ===================================================================================== []
            // DoSendZippedFeed
            private static MipResponse< MipSendZippedFeedFolderResult > DoSendZippedFeed( string feed, string reqId )
            {
                try {
                    UploadFiles(
                        LocalZippedFeedFile( feed, reqId ),
                        RemoteFeedOutgoingZipFilePath( feed, reqId )
                        );
                }
                catch( Exception exception ) {
                    return new MipResponse< MipSendZippedFeedFolderResult >(
                        false,
                        MipOperationStatus.SendZippedFeedFailure,
                        exception );
                }
                return new MipResponse< MipSendZippedFeedFolderResult >(
                    true,
                    MipOperationStatus.SendZippedFeedSuccess,
                    new MipSendZippedFeedFolderResult { MipRequestId = reqId } );
            }

            // ===================================================================================== []
            // GetRemoteDirFiles
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

            // ===================================================================================== []
            // FindFileNamePrefixInRemoteDir
            private static MipResponse< MipFindRemoteFileResult > FindRemoteFileNamePrefixInRemoteDir(
                string prefix,
                string remoteDir )
            {
                var files = GetRemoteDirFiles( remoteDir );
                foreach( RemoteFileInfo fileInfo in files ) {
                    if( fileInfo.Name.Contains( prefix ) ) {
                        return new MipResponse< MipFindRemoteFileResult >(
                            true,
                            MipOperationStatus.FindRemoteFileSuccess,
                            new MipFindRemoteFileResult( remoteDir, fileInfo.Name )
                            );
                    }
                }
                return new MipResponse< MipFindRemoteFileResult >(
                    false,
                    MipOperationStatus.FindRemoteFileFailure,
                    string.Format("Remote file [{0}] not found in [{1}]", prefix, remoteDir)
                    );
            }

            // ===================================================================================== []
            // UploadFiles
            private static void UploadFiles( string localPath, string remotePath )
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

            // ===================================================================================== []
            // UploadFiles
            private static void DownloadFiles( string remotePath, string localPath )
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

            // ===================================================================================== []
            // SessionOptions
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

            // ===================================================================================== []
            // TransferOptions
            private static TransferOptions TransferOptions()
            {
                return new TransferOptions {
                    TransferMode = TransferMode.Binary
                };
            }
        }
    }
}