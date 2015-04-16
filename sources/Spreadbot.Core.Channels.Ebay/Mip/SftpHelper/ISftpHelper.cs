// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels.Ebay
// ISftpHelper.cs

using Spreadbot.Core.Channels.Ebay.Mip.Operations.Response;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Results;

namespace Spreadbot.Core.Channels.Ebay.Mip.SftpHelper
{
    public interface ISftpHelper
    {
        MipResponse< MipSftpSendFilesResult > SendFiles( string localFiles, string remoteFiles );

        MipResponse< MipFindRemoteFileResult > FindRemoteFile(
            string filePrefix,
            string remoteDir );

        string GetRemoteFileContent( string remoteFolder, string fileName, string localFolder );
        void DownloadFiles( string remotePath, string localPath );

        MipResponse< MipFindRemoteFileResult > FindRemoteFile(
            string filePrefix,
            string[] remoteDirs );

        MipResponse< MipTestConnectionResult > TestConnection( string password = null );
    }
}