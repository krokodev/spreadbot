using System;
using Spreadbot.Core.Common;

namespace Spreadbot.Core.Mip
{
    public class Response<TR> : GenericResponse<TR, StatusCode> where TR : IResponseResult
    {
        public Response(bool isSucces, StatusCode code)
            : base(isSucces, code)
        {
        }

        public Response(bool isSucces, StatusCode code, Exception exception)
            : base(isSucces, code, exception)
        {
        }

        public Response(bool isSucces, StatusCode code, TR result)
            : base(isSucces, code, result)
        {
        }

        public Response(bool isSucces, StatusCode code, TR result, IResponse innerResponse)
            : base(isSucces, code, result, innerResponse)
        {
        }

        public Response(bool isSucces, StatusCode code, string details)
            : base(isSucces, code, details)
        {
        }
    }
}