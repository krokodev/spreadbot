using Crocodev.Common;

namespace Spreadbot.Core.Mip
{
    public class SendingFeedResult : IResponseResult
    {
        public SendingFeedResult(Request.Identifier requestId)
        {
            RequestId = requestId;
        }

        public string GetDescription(string format)
        {
            return format.SafeFormat("RequestId", RequestId);
        }

        public Request.Identifier RequestId { get; set; }
    }
}