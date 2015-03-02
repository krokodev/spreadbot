using System;

namespace Spreadbot.Core.Mip
{
    public partial class Connector
    {
        // ===================================================================================== []
        // SendFeed
        public static Response SendFeed(Feed feed)
        {
            var reqId = Request.GenerateId();
            try
            {
                ZipHelper.ZipFeed(feed, reqId).Check();
                SftpHelper.SendZippedFeed(feed, reqId).Check();
            }
            catch (Exception e)
            {
                return FailedResponse(StatusCode.SendFeedFail, e);
            }
            return SuccessfulResponse(StatusCode.SendFeedSuccess, reqId);
        }
        
        // ===================================================================================== []
        // FindRequest
        public static Response FindRequest(Request request, RequestProcessingStage stage)
        {
            // Now: Connector.FindRequest
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
                    case RequestProcessingStage.All:
                        ftpResponce = SftpHelper.FindRequestRemoteFileNameAnywhere(request);
                        break;
                    default:
                        ftpResponce = new Response(false, StatusCode.FindRequestFail, "Wrong stage {0}", stage);
                        break;
                }
                ftpResponce.Check();
            }
            catch (Exception e)
            {
                return FailedResponse(StatusCode.FindRequestFail, e);
            }
            return SuccessfulResponse(StatusCode.FindRequestSuccess, ftpResponce.Result, ftpResponce);
        }
    }
}