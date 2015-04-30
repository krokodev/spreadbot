// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Amazon
// MwsResponse.cs

using System;
using Spreadbot.Core.Channels.Amazon.Services.Mws.Operations.StatusCode;
using Spreadbot.Sdk.Common.Operations.ResponseResults;
using Spreadbot.Sdk.Common.Operations.Responses;

namespace Spreadbot.Core.Channels.Amazon.Services.Mws.Operations.Response
{
    public class MwsResponse<TR> : GenericResponse< TR, MwsOperationStatus >
        where TR : IResponseResult
    {
        public MwsResponse() {}

        public MwsResponse( Exception exception )
            : base( exception ) {}
    }
}