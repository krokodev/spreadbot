// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// MipConnector.SftpHelper.cs
// Roman, 2015-03-31 1:26 PM

using System.IO;
using Crocodev.Common.Extensions;
using Spreadbot.Core.Channels.Ebay.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Request;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Response;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Results;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.StatusCode;

namespace Spreadbot.Core.Channels.Ebay.Mip.Connector
{
    public partial class MipConnector
    {
        public partial class SftpHelper
        {
            // ===================================================================================== []
            // TestConnection
            public static MipResponse< MipTestConnectionResult > TestConnection( string password = null )
            {
                return DoTestConnection( password );
            }

            // ===================================================================================== []
            // UploadFeed
            public static MipResponse< MipSendZippedFeedFolderResult > SendZippedFeed( string feed, string reqId )
            {
                return DoSendZippedFeed( feed, reqId );
            }

            public static MipResponse< MipSendZippedFeedFolderResult > SendZippedFeed(
                MipFeedHandler mipFeedHandler,
                string reqId )
            {
                return SendZippedFeed( mipFeedHandler.GetName(), reqId );
            }

            // ===================================================================================== []
            // Find remote files Inproc
            public static MipResponse< MipFindRemoteFileResult > FindRequestRemoteFileNameInInprocess(
                MipRequestHandler mipRequestHandler )
            {
                var remoteDir = RemoteFeedInprocessFolderPath( mipRequestHandler.MipFeedHandler.GetName() );
                var prefix = mipRequestHandler.FileNamePrefix();

                return FindRemoteFileNamePrefixInRemoteDir( prefix, remoteDir );
            }

            // ===================================================================================== []
            // Find remote files Output
            public static MipResponse< MipFindRemoteFileResult > FindRequestRemoteFileNameInOutput(
                MipRequestHandler mipRequestHandler )
            {
                var remoteDirs = RemoteFeedOutputFolderPathes( mipRequestHandler.MipFeedHandler.GetName() );
                var prefix = mipRequestHandler.FileNamePrefix();

                foreach( var remoteDir in remoteDirs ) {
                    var response = FindRemoteFileNamePrefixInRemoteDir( prefix, remoteDir );
                    if( response.Code == MipOperationStatus.FindRemoteFileSuccess ) {
                        return response;
                    }
                }
                return new MipResponse< MipFindRemoteFileResult >(
                    false,
                    MipOperationStatus.FindRemoteFileFailure,
                    string.Format( "Remote file [{0}] not found in [{1}]", prefix, remoteDirs.FoldToStringBy( s => s ) ) );
            }

            // ===================================================================================== []
            // GetRemoteFileContent
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