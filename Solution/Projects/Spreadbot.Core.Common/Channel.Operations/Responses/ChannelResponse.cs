// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Common
// ChannelResponse.cs
// romak_000, 2015-03-19 13:43

using System;
using Spreadbot.Sdk.Common.Operations.ResponseResults;
using Spreadbot.Sdk.Common.Operations.Responses;

namespace Spreadbot.Core.Common.Channel.Operations.Responses
{
    public class ChannelResponse<TR> :
        GenericResponse<TR, ChannelResponseStatusCode>, IChannelResponse
        where TR : IResponseResult
    {
        public ChannelResponse()
        {
        }

        public ChannelResponse(bool isSucces, ChannelResponseStatusCode code, Exception exception)
            : base(isSucces, code, exception)
        {
        }

        public ChannelResponse(bool isSucces, ChannelResponseStatusCode code, TR result)
            : base(isSucces, code, result)
        {
        }

        public ChannelResponse(bool isSucces, ChannelResponseStatusCode code, TR result, IResponse inner)
            : base(isSucces, code, result, inner)
        {
        }
    }
}