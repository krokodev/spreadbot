using System;
using System.Diagnostics;
using Crocodev.Common;

// >> Connector

namespace Spreadbot.Core.Mip
{
    public partial class Connector
    {
        // ===================================================================================== []
        // SendFeed
        public static Response SendFeed(Feed feed, Request.Identifier reqId = null)
        {
            reqId = reqId ?? Request.GenerateId();
            try
            {
                ZipHelper.ZipFeed(feed, reqId).Check();
                SftpHelper.SendZippedFeed(feed, reqId).Check();
            }
            catch (Exception e)
            {
                return ResponseFail(StatusCode.SendFeedFail, e);
            }
            return ResponseSuccess(StatusCode.SendFeedSuccess, reqId);
        }

        public static Response SendTestFeed(Feed feed)
        {
            return SendFeed(feed, Request.GenerateTestId());
        }

        // ===================================================================================== []
        // FindRequest
        public static Response FindRequest(Request request, RequestProcessingStage stage)
        {
            Response ftpResponce;
            try
            {
                switch (stage)
                {
                    case RequestProcessingStage.Inprocess:
                        ftpResponce = SftpHelper.FindRequestRemoteFileNameInInprocess(request);
                        break;
                    case RequestProcessingStage.Output:
                        ftpResponce = SftpHelper.FindRequestRemoteFileNameInOutput(request);
                        break;
                    default:
                        throw new Exception("Wrong stage {0}".SafeFormat(stage));
                }
                ftpResponce.Check();
            }
            catch (Exception e)
            {
                return ResponseFail(StatusCode.FindRequestFail, e);
            }
            return ResponseSuccess(StatusCode.FindRequestSuccess, ftpResponce.Result, ftpResponce);
        }

        // ===================================================================================== []
        // GetRequestStatus
        public static Response GetRequestStatus(Request request)
        {
            // Now: GetRequestStatus
            try
            {
                var response = FindRequest(request, RequestProcessingStage.Inprocess);
                if (response.StatusCode == StatusCode.FindRequestSuccess)
                {
                    return ResponseSuccess(StatusCode.GetRequestStatusSuccess, GetRequetStatusResult.Inprocess);
                }

                response = FindRequest(request, RequestProcessingStage.Output);
                if (response.StatusCode == StatusCode.FindRequestSuccess)
                {
                    return GetRequestOutputStatus(response);
                }

                return ResponseSuccess(StatusCode.GetRequestStatusSuccess, GetRequetStatusResult.Unknown, response);
            }
            catch (Exception e)
            {
                return ResponseFail(StatusCode.GetRequestStatusFail, e);
            }
        }

        // --------------------------------------------------------[]
        private static Response GetRequestOutputStatus(Response response)
        {
            GetRequetStatusResult statusResult;
            string description;
            ReadRequestOutputStatus(response, out statusResult, out description);
            return ResponseSuccess(StatusCode.GetRequestStatusSuccess, statusResult, description);
        }

        // --------------------------------------------------------[]
        private static void ReadRequestOutputStatus(Response response, out GetRequetStatusResult statusResult, out string description)
        {
            throw new NotImplementedException();
/*            var fileName = (string) response.Result;
            var remotePath = RemoteFeedOutputFolderPath();
            var localPath = LocalRequestResultsFolder();
            var content = SftpHelper.GetRemoteFileContent(
                , 
                
                );
            ParseRequestContent(content, out status, out description);

            Trace.Assert(
                status == RequetStatus.Fail || status == RequetStatus.Success,
                "Unknown status=[{0}]".SafeArgs(status)
                );*/
        }

        // --------------------------------------------------------[]
        private static void ParseRequestContent(string content, out GetRequetStatusResult statusResult, out string description)
        {
            // Todo: Parse XML
            statusResult = content.Contains("<status>SUCCESS</status>")
                ? GetRequetStatusResult.Success
                : GetRequetStatusResult.Fail;
            description = content;
        }
    }
}