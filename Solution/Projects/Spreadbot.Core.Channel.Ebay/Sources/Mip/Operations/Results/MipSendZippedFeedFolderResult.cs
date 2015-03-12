using Crocodev.Common;
using Spreadbot.Core.Common;

namespace Spreadbot.Core.Channel.Ebay.Mip
{
    public class MipSendZippedFeedFolderResult : MipResponseResult
    {
        public MipSendZippedFeedFolderResult(MipRequest.Identifier requestId)
        {
            RequestId = requestId;
        }

        public override string GetAutoinfo()
        {
            return Template.SafeFormat("RequestId", RequestId);
        }

        public MipRequest.Identifier RequestId { get; set; }
    }
}