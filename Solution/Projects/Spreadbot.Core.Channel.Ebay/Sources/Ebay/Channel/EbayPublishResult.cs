using Crocodev.Common;
using Spreadbot.Core.Common;

namespace Spreadbot.Core.Channel.Ebay
{
    public class EbayPublishResult : ResponseResult
    {
        // Code: * ResponseResult
        // Todo: Request.Identifier 
        private readonly IResponse _mipResponse;

        public EbayPublishResult(IResponse mipResponse)
        {
            _mipResponse = mipResponse;
        }

        public override string GetAutoinfo()
        {
            return Template.SafeFormat("MipResponse", _mipResponse);
        }
    }
}