using System.IO;
using Crocodev.Common;
using Spreadbot.Core.Common;

namespace Spreadbot.Core.Channel.Ebay.Mip
{
    public partial class MipConnector
    {
        public partial class SftpHelper
        {
            // ===================================================================================== []
            // TestConnection
            public static MipResponse<BoolResult> TestConnection(string password = null)
            {
                return DoTestConnection(password);
            }

            // ===================================================================================== []
            // UploadFeed
            public static MipResponse<SendZippedFeedFolderResult> SendZippedFeed(string feed, Request.Identifier reqId)
            {
                return DoSendZippedFeed(feed, reqId);
            }

            public static MipResponse<SendZippedFeedFolderResult> SendZippedFeed(Feed feed, Request.Identifier reqId)
            {
                return SendZippedFeed(feed.Name, reqId);
            }

            // ===================================================================================== []
            // Find remote files Inproc
            public static MipResponse<FindRemoteFileResult> FindRequestRemoteFileNameInInprocess(Request request)
            {
                var remoteDir = RemoteFeedInprocessFolderPath(request.Feed.Name);
                var prefix = request.FileNamePrefix();

                return FindRemoteFileNamePrefixInRemoteDir(prefix, remoteDir);
            }

            // ===================================================================================== []
            // Find remote files Output
            public static MipResponse<FindRemoteFileResult> FindRequestRemoteFileNameInOutput(Request request)
            {
                var remoteDirs = RemoteFeedOutputFolderPathes(request.Feed.Name);
                var prefix = request.FileNamePrefix();

                foreach (var remoteDir in remoteDirs)
                {
                    var response = FindRemoteFileNamePrefixInRemoteDir(prefix, remoteDir);
                    if (response.Code == MipStatusCode.FindRemoteFileSuccess)
                    {
                        return response;
                    }
                }
                return new MipResponse<FindRemoteFileResult>(
                    false,
                    MipStatusCode.FindRemoteFileFail,
                    "Remote file [{0}] not found in [{1}]".SafeFormat(prefix, remoteDirs.FoldToStringBy(s => s)));
            }

            // ===================================================================================== []
            // GetRemoteFileContent
            public static string GetRemoteFileContent(string remoteFolder, string fileName, string localFolder)
            {
                var remotePath = @"{0}/{1}".SafeFormat(remoteFolder, fileName);
                var localPath = @"{0}\{1}".SafeFormat(localFolder, fileName);

                DownloadFiles(remotePath, localPath);

                return File.ReadAllText(@"{0}\{1}".SafeFormat(localFolder, fileName));
            }
        }
    }
}