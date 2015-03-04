using System;
using Crocodev.Common;

// >> | Core | Connector

namespace Spreadbot.Core.Mip
{
    public partial class Connector
    {
        // ===================================================================================== []
        // SendFeed
        public static Response<SendingFeedResult> SendFeed(Feed feed, Request.Identifier reqId = null)
        {
            reqId = reqId ?? Request.GenerateId();
            try
            {
                ZipHelper.ZipFeed(feed, reqId).Check();
                SftpHelper.SendZippedFeed(feed, reqId).Check();
            }
            catch (Exception e)
            {
                return Response<SendingFeedResult>.NewFail(StatusCode.SendFeedFail, e);
            }
            return Response<SendingFeedResult>.NewSuccess(StatusCode.SendFeedSuccess, new SendingFeedResult(reqId));
        }

        public static Response<SendingFeedResult> SendTestFeed(Feed feed)
        {
            return SendFeed(feed, Request.GenerateTestId());
        }

        // ===================================================================================== []
        // FindRequest
        public static Response<FindingRemoteFileResult> FindRequest(Request request, RequestProcessingStage stage)
        {
            Response<FindingRemoteFileResult> findResponse;
            try
            {
                switch (stage)
                {
                    case RequestProcessingStage.Inprocess:
                        findResponse = SftpHelper.FindRequestRemoteFileNameInInprocess(request);
                        break;
                    case RequestProcessingStage.Output:
                        findResponse = SftpHelper.FindRequestRemoteFileNameInOutput(request);
                        break;
                    default:
                        throw new Exception("Wrong stage {0}".SafeFormat(stage));
                }
                findResponse.Check();
            }
            catch (Exception e)
            {
                return Response<FindingRemoteFileResult>.NewFail(StatusCode.FindRequestFail, e);
            }
            return Response<FindingRemoteFileResult>.NewSuccess(StatusCode.FindRequestSuccess, findResponse.Result, findResponse);
        }

        // ===================================================================================== []
        // GetRequestStatus
        public static Response<GettingRequestStatusResult> GetRequestStatus(Request request, bool ignoreInprocess=false)
        {
            try
            {
                var response = FindRequest(request, RequestProcessingStage.Inprocess);
                if (response.Code == StatusCode.FindRequestSuccess && !ignoreInprocess)
                {
                    return Response<GettingRequestStatusResult>.NewSuccess(
                        StatusCode.GetRequestStatusSuccess,
                        new GettingRequestStatusResult(RequetStatus.Inprocess)
                        );
                }

                response = FindRequest(request, RequestProcessingStage.Output);
                if (response.Code == StatusCode.FindRequestSuccess)
                {
                    return GetRequestOutputStatus(response);
                }

                return Response<GettingRequestStatusResult>.NewSuccess(
                    StatusCode.GetRequestStatusSuccess,
                    new GettingRequestStatusResult(RequetStatus.Unknown),
                    response
                    );
            }
            catch (Exception e)
            {
                return Response<GettingRequestStatusResult>.NewFail(StatusCode.GetRequestStatusFail, e);
            }
        }

        // --------------------------------------------------------[]
        private static Response<GettingRequestStatusResult> GetRequestOutputStatus(Response<FindingRemoteFileResult> response)
        {
            var statusResult = ReadRequestOutputStatus(response);
            return Response<GettingRequestStatusResult>.NewSuccess(StatusCode.GetRequestStatusSuccess, statusResult);
        }

        // --------------------------------------------------------[]
        private static GettingRequestStatusResult ReadRequestOutputStatus(Response<FindingRemoteFileResult> response)
        {
            var fileName = response.Result.FileName;
            var remotePath = response.Result.FolderPath;
            var localPath = LocalRequestResultsFolder();
            var content = SftpHelper.GetRemoteFileContent(remotePath, fileName, localPath);
            return ParseRequestContent(content);
        }

        // --------------------------------------------------------[]
        private static GettingRequestStatusResult ParseRequestContent(string content)
        {
            // Todo: Later : Parse XML
            return new GettingRequestStatusResult(
                content.Contains("<status>SUCCESS</status>")
                    ? RequetStatus.Success
                    : RequetStatus.Fail,
                content);
        }
    }
}