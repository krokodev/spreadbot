using Crocodev.Common;

namespace Spreadbot.Core.Channel.Ebay.Mip
{
    public class MipSendZippedFeedFolderResult : AbstractMipResponseResult
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

        public override MipRequest.Identifier GetMipRequestId()
        {
            return MipRequestId;
        }

        public MipRequest.Identifier MipRequestId { get; set; }
    }
}