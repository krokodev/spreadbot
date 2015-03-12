using Crocodev.Common;
using Spreadbot.Core.Channel.Ebay.Mip;
using Spreadbot.Core.Common;

namespace Spreadbot.Core.Channel.Ebay
{
    public class EbayPublishResult : MipResponseResult
    {
        // Code: * ResponseResult
        // Todo: Request.Identifier 
        private readonly MipRequest.Identifier _mipRequestId;

        public EbayPublishResult(MipRequest.Identifier mipRequestId)
        {
            _mipRequestId = mipRequestId;
        }

        public override string GetAutoinfo()
        {
            return Template.SafeFormat("MipRequestId", _mipRequestId);
        }
    }
}