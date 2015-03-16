using Crocodev.Common;
using Spreadbot.Core.Channel.Ebay.Mip;
using Spreadbot.Core.Common;

namespace Spreadbot.Core.Channel.Ebay
{
    public class EbayPublishResult : MipResponseResult
    {
        public readonly MipRequest.Identifier MipRequestId;

        public EbayPublishResult()
        {
        }

        public EbayPublishResult(MipRequest.Identifier mipRequestId)
        {
            MipRequestId = mipRequestId;
        }

        public override string Autoinfo
        {
            get { return Template.SafeFormat("MipRequestId", MipRequestId); }
        }
    }
}