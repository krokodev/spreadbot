// Spreadbot (c) 2015 Crocodev
// Spreadbot.Sdk.Common
// ResponseException.cs
// romak_000, 2015-03-19 15:49

using Spreadbot.Sdk.Common.Operations.Responses;

namespace Spreadbot.Sdk.Common.Exceptions
{
    public class ResponseException : SpreadbotException
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