using Crocodev.Common;
using Spreadbot.Core.Common;

namespace Spreadbot.Core.Channel.Ebay.Mip
{
    public class SendFeedFolderResult : IResponseResult
    {
        public SendFeedFolderResult(Request.Identifier requestId)
        {
            RequestId = requestId;
        }

        public string GetAutoinfo(string format)
        {
            return format.SafeFormat("RequestId", RequestId);
        }

        public Request.Identifier RequestId { get; set; }
    }
}