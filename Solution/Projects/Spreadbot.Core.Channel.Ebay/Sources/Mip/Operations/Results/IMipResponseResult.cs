using Spreadbot.Sdk.Common;

namespace Spreadbot.Core.Channel.Ebay.Mip
{
    public interface IMipResponseResult: IResponseResult
    {
        MipRequest.Identifier GetMipRequestId();
    }
}