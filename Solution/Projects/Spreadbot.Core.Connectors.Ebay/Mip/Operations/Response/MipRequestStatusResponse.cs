// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Connectors.Ebay
// MipRequestStatusResponse.cs
// romak_000, 2015-03-19 15:38

using System;
using Spreadbot.Core.Channel.Ebay.Mip.Operations.Results;
using Spreadbot.Core.Channel.Ebay.Mip.Operations.StatusCode;
using Spreadbot.Sdk.Common.Operations.Responses;
using Spreadbot.Sdk.Common.Operations.Tasks;

namespace Spreadbot.Core.Channel.Ebay.Mip.Operations.Response
{
    public class MipRequestStatusResponse : MipResponse<MipGetRequestStatusResult>, ITaskProceedInfo
    {
        public MipRequestStatusResponse(bool isSucces, MipStatusCode code) : base(isSucces, code)
        {
        }

        public MipRequestStatusResponse(bool isSucces, MipStatusCode code, Exception exception)
            : base(isSucces, code, exception)
        {
        }

        public MipRequestStatusResponse(bool isSucces, MipStatusCode code, MipGetRequestStatusResult result)
            : base(isSucces, code, result)
        {
        }

        public MipRequestStatusResponse(bool isSucces, MipStatusCode code, MipGetRequestStatusResult result,
            IResponse innerResponse) : base(isSucces, code, result, innerResponse)
        {
        }

        public MipRequestStatusResponse(bool isSucces, MipStatusCode code, string details)
            : base(isSucces, code, details)
        {
        }
    }
}