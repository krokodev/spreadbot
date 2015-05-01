// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Abstracts
// ChannelResponse.cs

using System;
using Spreadbot.Sdk.Common.Operations.ResponseResults;
using Spreadbot.Sdk.Common.Operations.Responses;

namespace Spreadbot.Core.Abstracts.Channel.Operations.Responses
{
    public class ChannelResponse<TR> :
        Response< TR >, IChannelResponse
        where TR : IResponseResult
    {
        public ChannelResponse() {}

        public ChannelResponse( Exception exception )
            : base( exception ) {}
    }
}