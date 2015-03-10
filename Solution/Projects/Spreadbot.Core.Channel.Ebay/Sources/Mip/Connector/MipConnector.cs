using System;
using Crocodev.Common;
using Crocodev.Common.Identifier;

// >> Core | Connector

namespace Spreadbot.Core.Channel.Ebay.Mip
{
    public partial class MipConnector
    {
        // ===================================================================================== []
        // SendFeed
        public static MipResponse<SendFeedResult> SendFeed(Feed feed)
        {
            var reqId = Request.GenerateId();
            try
            {
                ZipHelper.ZipFeed(feed, reqId).Check();
                SftpHelper.SendZippedFeed(feed, reqId).Check();
            }
            catch (Exception exception)
            {
                return new MipResponse<SendFeedResult>(false, MipStatusCode.SendFeedFail, exception);
            }
            return new MipResponse<SendFeedResult>(true, MipStatusCode.SendFeedSuccess, new SendFeedResult(reqId));
        }

        // --------------------------------------------------------[]
        public static MipResponse<SendFeedResult> SendTestFeed(Feed feed)
        {
            var reqId = Request.GenerateTestId();
            return DoSendFeed(feed, reqId);
        }

        // --------------------------------------------------------[]
        private static MipResponse<SendFeedResult> DoSendFeed(Feed feed, Identifiable<Request, Guid>.Identifier reqId)
        {
            try
            {
                ZipHelper.ZipFeed(feed, reqId).Check();
                SftpHelper.SendZippedFeed(feed, reqId).Check();
            }
            catch (Exception exception)
            {
                return new MipResponse<SendFeedResult>(false, MipStatusCode.SendFeedFail, exception);
            }
            return new MipResponse<SendFeedResult>(true, MipStatusCode.SendFeedSuccess, new SendFeedResult(reqId));
        }

        // ===================================================================================== []
        // FindRequest
        public static MipResponse<FindRemoteFileResult> FindRequest(Request request, RequestProcessingStage stage)
        {
            MipResponse<FindRemoteFileResult> findResponse;
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
                return new MipResponse<FindRemoteFileResult>(false, MipStatusCode.FindRequestFail, exception);
            }
            return new MipResponse<FindRemoteFileResult>(true, MipStatusCode.FindRequestSuccess, findResponse.Result,
                findResponse);
        }

        // ===================================================================================== []
        // GetRequestStatus
        public static MipResponse<GetRequestStatusResult> GetRequestStatus(Request request, bool ignoreInprocess = false)
        {
            try
            {
                var response = FindRequest(request, RequestProcessingStage.Inprocess);
                if (response.Code == MipStatusCode.FindRequestSuccess && !ignoreInprocess)
                {
                    return new MipResponse<GetRequestStatusResult>(true,
                        MipStatusCode.GetRequestStatusSuccess,
                        new GetRequestStatusResult(RequetStatus.Inprocess)
                        );
                }

                response = FindRequest(request, RequestProcessingStage.Output);
                if (response.Code == MipStatusCode.FindRequestSuccess)
                {
                    return GetRequestOutputStatus(response);
                }

                return new MipResponse<GetRequestStatusResult>(true,
                    MipStatusCode.GetRequestStatusSuccess,
                    new GetRequestStatusResult(RequetStatus.Unknown),
                    response
                    );
            }
            catch (Exception exception)
            {
                return new MipResponse<GetRequestStatusResult>(false, MipStatusCode.GetRequestStatusFail, exception);
            }
        }

        // --------------------------------------------------------[]
        private static MipResponse<GetRequestStatusResult> GetRequestOutputStatus(
            MipResponse<FindRemoteFileResult> response)
        {
            var statusResult = ReadRequestOutputStatus(response);
            return new MipResponse<GetRequestStatusResult>(true, MipStatusCode.GetRequestStatusSuccess, statusResult);
        }

        // --------------------------------------------------------[]
        private static GetRequestStatusResult ReadRequestOutputStatus(MipResponse<FindRemoteFileResult> response)
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

        public static string NewFeedXmlFilePath(Feed feed)
        {
            return string.Format(@"{0}\{1}.{2}.xml",
                LocalFeedFolder(feed.Name),
                feed.Name,
                (Guid) feed.Id
                );
        }
    }
}