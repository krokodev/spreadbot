// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels.Ebay
// MipResponse.cs

using System;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.StatusCode;
using Spreadbot.Sdk.Common.Operations.ResponseResults;
using Spreadbot.Sdk.Common.Operations.Responses;

namespace Spreadbot.Core.Channels.Ebay.Mip.Operations.Response
{
    public class MipResponse<TR> : GenericResponse< TR, MipOperationStatus >
        where TR : IResponseResult
    {
        public MipResponse() {}

        public MipResponse( Exception exception )
            : base( exception ) {}
    }
}