// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Amazon
// MwsResponse.cs

using System;
using Spreadbot.Core.Channels.Amazon.Services.Mws.Operations.Statuses;
using Spreadbot.Sdk.Common.Operations.ResponseResults;
using Spreadbot.Sdk.Common.Operations.Responses;

namespace Spreadbot.Core.Channels.Amazon.Services.Mws.Operations.Responses
{
    public class MwsResponse<TR> : GenericResponse< TR, MwsOperationStatus >
        where TR : IResponseResult
    {
        public MwsResponse()
        {
            StatusCode = MwsOperationStatus.Success;
        }

        public MwsResponse( Exception exception )
            : base( exception )
        {
            StatusCode = MwsOperationStatus.Failure;
        }
    }
}