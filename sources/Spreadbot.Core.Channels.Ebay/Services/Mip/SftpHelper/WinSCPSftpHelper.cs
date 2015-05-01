// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// WinSCPSftpHelper.cs

using Spreadbot.Core.Channels.Ebay.Services.Mip.Operations.Responses;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Operations.Results;

namespace Spreadbot.Core.Channels.Ebay.Services.Mip.SftpHelper
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
        public virtual MipResponse< MipFindRemoteFileResult > FindRemoteFile(
            string filePrefix,
            string remoteDir )
        {
            return _FindRemoteFile( filePrefix, remoteDir );
        }

        // --------------------------------------------------------[]
        public virtual MipResponse< MipFindRemoteFileResult > FindRemoteFile(
            string filePrefix,
            string[] remoteDirs )
        {
            return _FindRemoteFile( filePrefix, remoteDirs );
        }

        // --------------------------------------------------------[]
        public virtual string GetRemoteFileContent( string remoteFolder, string fileName, string localFolder )
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