// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// MipConnector.SftpHelper.cs
// Roman, 2015-04-03 1:45 PM

using System.IO;
using Crocodev.Common.Extensions;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Response;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Results;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.StatusCode;
using WinSCP;

namespace Spreadbot.Core.Channels.Ebay.Mip.Connector
{
    public partial class MipConnector
    {
        public partial class SftpHelper
        {
            // --------------------------------------------------------[]
            public static MipResponse< MipTestConnectionResult > TestConnection( string password = null )
            {
                return DoTestConnection( password );
            }

            // --------------------------------------------------------[]
            public static MipResponse< MipSftpSendFilesResult > SendFiles( string localFiles, string remoteFiles )
            {
                return DoSendFiles( localFiles, remoteFiles );
            }

            // --------------------------------------------------------[]
            public static MipResponse< MipFindRemoteFileResult > FindRemoteFile(
                string filePrefix,
                string remoteDir )
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
            public static MipResponse< MipFindRemoteFileResult > FindRemoteFile(
                string filePrefix,
                string[] remoteDirs )
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

            // --------------------------------------------------------[]
            public static string GetRemoteFileContent( string remoteFolder, string fileName, string localFolder )
            {
                var remotePath = string.Format( @"{0}/{1}", remoteFolder, fileName );
                var localPath = string.Format( @"{0}\{1}", localFolder, fileName );

                DownloadFiles( remotePath, localPath );

                return File.ReadAllText( string.Format( @"{0}\{1}", localFolder, fileName ) );
            }
        }
    }
}