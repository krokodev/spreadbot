using Crocodev.Common;
using Spreadbot.Core.Common;

namespace Spreadbot.Core.Channel.Ebay.Mip
{
    public class SendFeedResult : IResponseResult
    {
        public SendFeedResult(Request.Identifier requestId)
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