// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Abstracts
// ChannelResponse.cs
// Roman, 2015-04-01 4:58 PM

using System;
using Spreadbot.Sdk.Common.Operations.ResponseResults;
using Spreadbot.Sdk.Common.Operations.Responses;

namespace Spreadbot.Core.Abstracts.Channel.Operations.Responses
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