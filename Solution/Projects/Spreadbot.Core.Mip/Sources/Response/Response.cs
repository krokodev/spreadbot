using System;
using Crocodev.Common;

namespace Spreadbot.Core.Mip
{
    public class Response
    {
        // ===================================================================================== []
        // Public
        public Response(bool isSucces = false, StatusCode statusCode = StatusCode.Unknown, string statusDescription = "",
            params object[] args)
        {
            IsSuccess = isSucces;
            StatusCode = statusCode;
            StatusDescription = statusDescription.TryFormat(args);
        }

        public StatusCode StatusCode { get; set; }
        public string StatusDescription { get; set; }
        public Request.Identifier RequestId { get; set; }
        public object Result { get; set; }

        public void Check()
        {
            if (!IsSuccess)
                throw new Exception(
                    string.Format("StatusCode=[{0}] StatusDescription=[{1}]",
                        StatusCode,
                        StatusDescription)
                    );
        }

        // ===================================================================================== []
        // Privarte
        private bool IsSuccess { get; set; }
    }
}