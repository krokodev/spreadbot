// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Abstracts
// ChannelResponse.cs
// Roman, 2015-04-03 8:16 PM

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

        public ChannelResponse( Exception exception )
            : base( exception ) {}
    }
}