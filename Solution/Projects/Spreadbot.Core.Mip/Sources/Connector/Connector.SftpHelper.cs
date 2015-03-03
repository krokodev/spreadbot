using System;
using Crocodev.Common;

namespace Spreadbot.Core.Mip
{
    public partial class Connector
    {
        public partial class SftpHelper
        {
            // ===================================================================================== []
            // TestConnection
            public static Response<TestingConnectionResult> TestConnection(string password = null)
            {
                return DoTestConnection(password);
            }

            // ===================================================================================== []
            // UploadFeed
            public static Response<SendingFeedResult> SendZippedFeed(string feed, string reqId)
            {
                return DoSendZippedFeed(feed, reqId);
            }

            public static Response<SendingFeedResult> SendZippedFeed(Feed feed, Request.Identifier reqId)
            {
                return SendZippedFeed(feed.Name, reqId.Value.ToString());
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
            public static string GetRemoteFileContent(string remoteFileName, string localFileName)
            {
                throw new NotImplementedException();
            }



            /*

            # ============================================================================================== []
            function GetRemoteFileContent([string]$remoteDir, [string]$fileName)
            {
                Debug "Downloading [$localPath\$fileName]"

                $localPath = $LocalInboxPath
                DownloadFile $remoteDir $fileName $localPath ([WinSCP.TransferMode]::Ascii)
                gc "$localPath\$fileName"
            }

            # ============================================================================================== []
            function DownloadFile ([string]$remoteDir, [string]$fileName, [string]$localPath, [WinSCP.TransferMode]$transferMode=[WinSCP.TransferMode]::Binary)
            {
                $session         = New-Object WinSCP.Session
                $transferOptions = New-Object WinSCP.TransferOptions
                $transferOptions.TransferMode = $transferMode
                try
                {
                    $session.Open($SessionOptions) 
                    $Session.GetFiles("$remoteDir/$fileName", "$localPath\$fileName", $false, $transferOptions).Check()
                }
                finally
                {
                    $session.Dispose()
                }
            }
             */
        }
    }
}