// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// WinSCPSftpHelper.cs
// Roman, 2015-04-06 5:04 PM

using Spreadbot.Core.Channels.Ebay.Mip.Connector;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Response;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Results;

namespace Spreadbot.Core.Channels.Ebay.Mip.SftpHelper
{
    public partial class WinScpSftpHelper : ISftpHelper
    {
        // --------------------------------------------------------[]
        public MipResponse< MipTestConnectionResult > TestConnection( string password = null )
        {
            return DoTestConnection( password );
        }

        // --------------------------------------------------------[]
        public MipResponse< MipSftpSendFilesResult > SendFiles( string localFiles, string remoteFiles )
        {
            return DoSendFiles( localFiles, remoteFiles );
        }

        // --------------------------------------------------------[]
        public MipResponse< MipFindRemoteFileResult > FindRemoteFile(
            string filePrefix,
            string remoteDir )
        {
            return _FindRemoteFile( filePrefix, remoteDir );
        }

        // --------------------------------------------------------[]
        public MipResponse< MipFindRemoteFileResult > FindRemoteFile(
            string filePrefix,
            string[] remoteDirs )
        {
            return _FindRemoteFile( filePrefix, remoteDirs );
        }

        // --------------------------------------------------------[]
        public string GetRemoteFileContent( string remoteFolder, string fileName, string localFolder )
        {
            return _GetRemoteFileContent( remoteFolder, fileName, localFolder );
        }

        // --------------------------------------------------------[]
        public void DownloadFiles( string remotePath, string localPath )
        {
            _DownloadFiles( remotePath, localPath );
        }
    }
}