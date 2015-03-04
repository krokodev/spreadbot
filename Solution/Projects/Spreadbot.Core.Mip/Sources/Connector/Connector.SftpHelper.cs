﻿using System.IO;
using Crocodev.Common;

namespace Spreadbot.Core.Mip
{
    public partial class Connector
    {
        public partial class SftpHelper
        {
            // ===================================================================================== []
            // TestConnection
            public static Response<BoolResult> TestConnection(string password = null)
            {
                return DoTestConnection(password);
            }

            // ===================================================================================== []
            // UploadFeed
            public static Response<SendingFeedResult> SendZippedFeed(string feed, Request.Identifier reqId)
            {
                return DoSendZippedFeed(feed, reqId);
            }

            public static Response<SendingFeedResult> SendZippedFeed(Feed feed, Request.Identifier reqId)
            {
                return SendZippedFeed(feed.Name, reqId);
            }

            // ===================================================================================== []
            // Find remote files Inproc
            public static Response<FindingRemoteFileResult> FindRequestRemoteFileNameInInprocess(Request request)
            {
                var remoteDir = RemoteFeedInprocessFolderPath(request.Feed.Name);
                var prefix = request.FileNamePrefix();

                return FindRemoteFileNamePrefixInRemoteDir(prefix, remoteDir);
            }

            // ===================================================================================== []
            // Find remote files Output
            public static Response<FindingRemoteFileResult> FindRequestRemoteFileNameInOutput(Request request)
            {
                var remoteDirs = RemoteFeedOutputFolderPathes(request.Feed.Name);
                var prefix = request.FileNamePrefix();

                foreach (var remoteDir in remoteDirs)
                {
                    var response = FindRemoteFileNamePrefixInRemoteDir(prefix, remoteDir);
                    if (response.Code == StatusCode.FindRemoteFileSuccess)
                    {
                        return response;
                    }
                }
                return Response<FindingRemoteFileResult>.NewFail(
                    StatusCode.FindRemoteFileFail,
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