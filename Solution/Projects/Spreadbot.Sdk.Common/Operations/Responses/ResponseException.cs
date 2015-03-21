// Spreadbot (c) 2015 Crocodev
// Spreadbot.Sdk.Common
// ResponseException.cs
// romak_000, 2015-03-21 2:13

using Spreadbot.Sdk.Common.Exceptions;

namespace Spreadbot.Sdk.Common.Operations.Responses
{
    public class ResponseException : SpreadbotException
    {
        public ResponseException( IAbstractResponse response )
        {
            Response = response;
        }

        public override string Message
        {
            get { return Response.Autoinfo; }
        }

        public IAbstractResponse Response { get; set; }
    }
}