using Crocodev.Common;
using Spreadbot.Core.Common;

namespace Spreadbot.Core.Channel.Ebay.Mip
{
    public class SendZippedFeedFolderResult : ResponseResult
    {
        public SendZippedFeedFolderResult(Request.Identifier requestId)
        {
            RequestId = requestId;
        }

        public override string GetAutoinfo()
        {
            return Template.SafeFormat("RequestId", RequestId);
        }

        public Request.Identifier RequestId { get; set; }
    }
}