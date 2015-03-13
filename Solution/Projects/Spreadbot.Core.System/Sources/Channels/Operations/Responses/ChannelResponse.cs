using System;
using Spreadbot.Sdk.Common;

namespace Spreadbot.Core.System
{
    public class ChannelResponse<TR> : GenericResponse<TR, ChannelResponseStatusCode> where TR : IResponseResult
    {
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