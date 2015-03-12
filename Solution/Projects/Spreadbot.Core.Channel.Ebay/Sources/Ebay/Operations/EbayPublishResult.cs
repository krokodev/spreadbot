using Crocodev.Common;
using Spreadbot.Core.Channel.Ebay.Mip;
using Spreadbot.Core.Common;

namespace Spreadbot.Core.Channel.Ebay
{
    public class EbayPublishResult : MipResponseResult
    {
        private readonly MipRequest.Identifier _mipRequestId;

        public EbayPublishResult(MipRequest.Identifier mipRequestId)
        {
            _mipRequestId = mipRequestId;
        }

        // Code: * EbayPublishResul : GetAutoinfo
        public override string Autoinfo
        {
            get { return Template.SafeFormat("MipRequestId", _mipRequestId); }
        }
    }
}