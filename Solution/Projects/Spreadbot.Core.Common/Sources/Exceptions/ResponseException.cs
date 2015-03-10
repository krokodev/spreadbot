using System;

namespace Spreadbot.Core.Common
{
    public class ResponseException: Exception
    {
        public ResponseException(IResponse response)
        {
            Response = response;
        }

        public override string Message
        {
            get { return Response.Autoinfo; }
        }

        public IResponse Response { get; set; }
    }
}