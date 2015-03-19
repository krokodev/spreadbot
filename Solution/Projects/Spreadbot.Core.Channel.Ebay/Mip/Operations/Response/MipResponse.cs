// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Connectors.Ebay
// MipResponse.cs
// romak_000, 2015-03-19 14:02

using System;
using Spreadbot.Core.Channel.Ebay.Mip.Operations.Results;
using Spreadbot.Core.Channel.Ebay.Mip.Operations.StatusCode;
using Spreadbot.Sdk.Common.Operations.Responses;

namespace Spreadbot.Core.Channel.Ebay.Mip.Operations.Response
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

        public Guid GetMipRequestId()
        {
            return Result.GetMipRequestId();
        }
    }
}