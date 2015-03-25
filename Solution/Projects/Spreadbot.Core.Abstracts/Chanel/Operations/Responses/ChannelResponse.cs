// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Abstracts
// ChannelResponse.cs
// romak_000, 2015-03-25 15:24

using System;
using Spreadbot.Sdk.Common.Operations.ResponseResults;
using Spreadbot.Sdk.Common.Operations.Responses;

namespace Spreadbot.Core.Abstracts.Chanel.Operations.Responses
{
    public class ChannelResponse<TR> :
        GenericResponse< TR, ChannelResponseStatusCode >, IChannelResponse
        where TR : IResponseResult
    {
        public ChannelResponse() {}

        public ChannelResponse( bool isSucces, ChannelResponseStatusCode code, Exception exception )
            : base( isSucces, code, exception ) {}

        public ChannelResponse( bool isSucces, ChannelResponseStatusCode code, TR result )
            : base( isSucces, code, result ) {}

        public ChannelResponse( bool isSucces, ChannelResponseStatusCode code, TR result, IAbstractResponse inner )
            : base( isSucces, code, result, inner ) {}
    }
}