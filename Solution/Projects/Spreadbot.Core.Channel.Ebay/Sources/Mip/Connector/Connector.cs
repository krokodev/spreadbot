using System;
using Crocodev.Common;
using Crocodev.Common.Identifier;

// >> | Core | Connector

namespace Spreadbot.Core.Channel.Ebay.Mip
{
    public partial class Connector
    {
        // ===================================================================================== []
        // SendFeed
        public static Response<SendFeedResult> SendFeed(Feed feed)
        {
            var reqId = Request.GenerateId();
            try
            {
                ZipHelper.ZipFeed(feed, reqId).Check();
                SftpHelper.SendZippedFeed(feed, reqId).Check();
            }
            catch (Exception exception)
            {
                return new Response<SendFeedResult>(false, StatusCode.SendFeedFail, exception);
            }
            return new Response<SendFeedResult>(true, StatusCode.SendFeedSuccess, new SendFeedResult(reqId));
        }

        // --------------------------------------------------------[]
        public static Response<SendFeedResult> SendTestFeed(Feed feed)
        {
            var reqId = Request.GenerateTestId();
            return DoSendFeed(feed, reqId);
        }

        // --------------------------------------------------------[]
        private static Response<SendFeedResult> DoSendFeed(Feed feed, Identifiable<Request, Guid>.Identifier reqId)
        {
            try
            {
                ZipHelper.ZipFeed(feed, reqId).Check();
                SftpHelper.SendZippedFeed(feed, reqId).Check();
            }
            catch (Exception exception)
            {
                return new Response<SendFeedResult>(false, StatusCode.SendFeedFail, exception);
            }
            return new Response<SendFeedResult>(true, StatusCode.SendFeedSuccess, new SendFeedResult(reqId));
        }

        // ===================================================================================== []
        // FindRequest
        public static Response<FindRemoteFileResult> FindRequest(Request request, RequestProcessingStage stage)
        {
            Response<FindRemoteFileResult> findResponse;
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
            catch (Exception exception)
            {
                return new Response<FindRemoteFileResult>(false, StatusCode.FindRequestFail, exception);
            }
            return new Response<FindRemoteFileResult>(true, StatusCode.FindRequestSuccess, findResponse.Result,
                findResponse);
        }

        // ===================================================================================== []
        // GetRequestStatus
        public static Response<GetRequestStatusResult> GetRequestStatus(Request request, bool ignoreInprocess = false)
        {
            try
            {
                var response = FindRequest(request, RequestProcessingStage.Inprocess);
                if (response.Code == StatusCode.FindRequestSuccess && !ignoreInprocess)
                {
                    return new Response<GetRequestStatusResult>(true,
                        StatusCode.GetRequestStatusSuccess,
                        new GetRequestStatusResult(RequetStatus.Inprocess)
                        );
                }

                response = FindRequest(request, RequestProcessingStage.Output);
                if (response.Code == StatusCode.FindRequestSuccess)
                {
                    return GetRequestOutputStatus(response);
                }

                return new Response<GetRequestStatusResult>(true,
                    StatusCode.GetRequestStatusSuccess,
                    new GetRequestStatusResult(RequetStatus.Unknown),
                    response
                    );
            }
            catch (Exception exception)
            {
                return new Response<GetRequestStatusResult>(false, StatusCode.GetRequestStatusFail, exception);
            }
        }

        // --------------------------------------------------------[]
        private static Response<GetRequestStatusResult> GetRequestOutputStatus(Response<FindRemoteFileResult> response)
        {
            var statusResult = ReadRequestOutputStatus(response);
            return new Response<GetRequestStatusResult>(true, StatusCode.GetRequestStatusSuccess, statusResult);
        }

        // --------------------------------------------------------[]
        private static GetRequestStatusResult ReadRequestOutputStatus(Response<FindRemoteFileResult> response)
        {
            var fileName = response.Result.FileName;
            var remotePath = response.Result.FolderPath;
            var localPath = LocalRequestResultsFolder();
            var content = SftpHelper.GetRemoteFileContent(remotePath, fileName, localPath);
            return ParseRequestContent(content);
        }

        // --------------------------------------------------------[]
        private static GetRequestStatusResult ParseRequestContent(string content)
        {
            // Todo: Later : Parse XML
            return new GetRequestStatusResult(
                content.Contains("<status>SUCCESS</status>")
                    ? RequetStatus.Success
                    : RequetStatus.Fail,
                content);
        }
    }
}