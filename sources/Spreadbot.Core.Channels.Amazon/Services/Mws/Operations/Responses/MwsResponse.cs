// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Amazon
// MwsResponse.cs

using System;
using Spreadbot.Sdk.Common.Operations.ResponseResults;
using Spreadbot.Sdk.Common.Operations.Responses;

namespace Spreadbot.Core.Channels.Amazon.Services.Mws.Operations.Responses
{
    public class MwsResponse<TR> : GenericResponse< TR >
        where TR : IResponseResult
    {
        public MwsResponse() {}

        public MwsResponse( Exception exception )
            : base( exception ) {}
    }
}