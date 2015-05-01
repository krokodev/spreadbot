// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// ISftpHelper.cs

using Spreadbot.Core.Channels.Ebay.Services.Mip.Operations.Results;
using Spreadbot.Sdk.Common.Operations.Responses;

namespace Spreadbot.Core.Channels.Ebay.Services.Mip.SftpHelper
{
    public interface ISftpHelper
    {
        Response< MipSftpSendFilesResult > SendFiles( string localFiles, string remoteFiles );

        Response< MipFindRemoteFileResult > FindRemoteFile(
            string filePrefix,
            string remoteDir );

        string GetRemoteFileContent( string remoteFolder, string fileName, string localFolder );
        void DownloadFiles( string remotePath, string localPath );

        Response< MipFindRemoteFileResult > FindRemoteFile(
            string filePrefix,
            string[] remoteDirs );

        Response< MipTestConnectionResult > TestConnection( string password = null );
    }
}