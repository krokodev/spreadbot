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
        public static Response<GettingRequestStatusResult> GetRequestStatus(Request request)
        {
            // Now: GetRequestStatus
            try
            {
                var response = FindRequest(request, RequestProcessingStage.Inprocess);
                if (response.Code == StatusCode.FindRequestSuccess)
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
        private static GettingRequestStatusResult ParseRequestContent(string content)
        {
            // Todo: Parse XML
            return new GettingRequestStatusResult(
                content.Contains("<status>SUCCESS</status>")
                    ? RequetStatus.Success
                    : RequetStatus.Fail,
                content);
        }
    }
}