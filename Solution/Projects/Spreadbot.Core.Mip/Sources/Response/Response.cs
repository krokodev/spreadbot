using System;

namespace Spreadbot.Core.Mip
{
    public class Response
    {
        public Response(bool isSucces=false, StatusCode statusCode = StatusCode.Unknown, string statusDescription="")
        {
            IsSuccess = isSucces;
            StatusCode = statusCode;
            StatusDescription = statusDescription;
        }
        public StatusCode StatusCode { get; set; }
        public string StatusDescription { get; set; }
        public Request.Identifier RequestId { get; set; }

        public void Check()
        {
            if (!IsSuccess)
                throw new Exception();
        }

        private bool IsSuccess { get; set; }
    }
}