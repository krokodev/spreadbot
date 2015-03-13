using System;
using Spreadbot.Sdk.Common;

namespace Spreadbot.Core.Channel.Ebay.Mip
{
    public class MipRequestStatusResponse : MipResponse<MipGetRequestStatusResult>, ITaskProceedInfo
    {
        public MipRequestStatusResponse(bool isSucces, MipStatusCode code) : base(isSucces, code)
        {
        }

        public MipRequestStatusResponse(bool isSucces, MipStatusCode code, Exception exception) : base(isSucces, code, exception)
        {
        }

        public MipRequestStatusResponse(bool isSucces, MipStatusCode code, MipGetRequestStatusResult result) : base(isSucces, code, result)
        {
        }

        public MipRequestStatusResponse(bool isSucces, MipStatusCode code, MipGetRequestStatusResult result, IResponse innerResponse) : base(isSucces, code, result, innerResponse)
        {
        }

        public MipRequestStatusResponse(bool isSucces, MipStatusCode code, string details) : base(isSucces, code, details)
        {
        }
    }
}