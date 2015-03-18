using System;
using Spreadbot.Sdk.Common;

namespace Spreadbot.Core.Channel.Ebay.Mip
{
    public class MipResponse<TR> : GenericResponse<TR, MipStatusCode>, IMipResponse
        where TR : IMipResponseResult
    {
        public MipResponse()
        {
        }

        public MipResponse(bool isSucces, MipStatusCode code)
            : base(isSucces, code)
        {
        }

        public MipResponse(bool isSucces, MipStatusCode code, Exception exception)
            : base(isSucces, code, exception)
        {
        }

        public MipResponse(bool isSucces, MipStatusCode code, TR result)
            : base(isSucces, code, result)
        {
        }

        public MipResponse(bool isSucces, MipStatusCode code, TR result, IResponse innerResponse)
            : base(isSucces, code, result, innerResponse)
        {
        }

        public MipResponse(bool isSucces, MipStatusCode code, string details)
            : base(isSucces, code, details)
        {
        }

        public MipRequest.Identifier GetMipRequestId()
        {
            return Result.GetMipRequestId();
        }
    }
}