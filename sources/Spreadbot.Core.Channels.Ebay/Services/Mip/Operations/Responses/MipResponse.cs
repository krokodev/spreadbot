// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// MipResponse.cs

using System;
using Spreadbot.Sdk.Common.Operations.ResponseResults;
using Spreadbot.Sdk.Common.Operations.Responses;

namespace Spreadbot.Core.Channels.Ebay.Services.Mip.Operations.Responses
{
    public class MipResponse<TR> : GenericResponse< TR >
        where TR : IResponseResult
    {
        public MipResponse() {}

        public MipResponse( Exception exception )
            : base( exception ) {}
    }
}