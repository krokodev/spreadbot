using Crocodev.Common;
using Spreadbot.Core.Common;

namespace Spreadbot.Core.Channel.Ebay.Mip
{
    public class MipSendZippedFeedFolderResult : MipResponseResult
    {
        public MipSendZippedFeedFolderResult()
        {
        }

        public MipSendZippedFeedFolderResult(MipRequest.Identifier mipRequestId)
        {
            MipRequestId = mipRequestId;
        }

        public override string Autoinfo
        {
            get 
            {
                return Template.SafeFormat("RequestId", MipRequestId);
            }
        }

        public MipRequest.Identifier MipRequestId { get; set; }
    }
}