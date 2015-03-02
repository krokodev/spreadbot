using System;
using Crocodev.Common;
using WinSCP;

namespace Spreadbot.Core.Mip
{
    public partial class Connector
    {
        public partial class SftpHelper
        {
            // ===================================================================================== []
            // TestConnection
            public static Response TestConnection(string password = null)
            {
                return DoTestConnection(password);
            }

            // ===================================================================================== []
            // UploadFeed
            public static Response SendZippedFeed(string feed, string reqId)
            {
                return DoSendZippedFeed(feed, reqId);
            }

            public static Response SendZippedFeed(Feed feed, Request.Identifier reqId)
            {
                return SendZippedFeed(feed.Name, reqId.Value.ToString());
            }

            // ===================================================================================== []
            // Find remote files Inproc
            public static Response FindRequestRemoteFileNameInInprocess(Request request)
            {
                var remoteDir = RemoteFeedInprocessFolderPath(request.Feed.Name);
                var prefix = request.FileNamePrefix();

                return FindRemoteFileNamePrefixInRemoteDir(prefix, remoteDir);
            }

            // ===================================================================================== []
            // Find remote files Output
            public static Response FindRequestRemoteFileNameInOutput(Request request)
            {
                var remoteDirs = RemoteFeedOutputFolderPathes(request.Feed.Name);
                var prefix = request.FileNamePrefix();

                foreach (var remoteDir in remoteDirs)
                {
                    var response = FindRemoteFileNamePrefixInRemoteDir(prefix, remoteDir);
                    if (response.StatusCode == StatusCode.FindRemoteFileSuccess)
                    {
                        return response;
                    }
                }
                return ResponseFail(
                    StatusCode.FindRemoteFileFail,
                    "Remote file [{0}] not found in [{1}]".SafeFormat(prefix, remoteDirs.FoldToStringBy(s => s)));
            }

            public static Response FindRequestRemoteFileNameAnywhere(Request request)
            {
                throw new NotImplementedException();
            }
        }
    }
}