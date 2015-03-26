// Spreadbot (c) 2015 Crocodev
// Spreadbot.Sdk.Common
// ResponseException.cs
// romak_000, 2015-03-26 19:42

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
            get { return Response.ToString(); }
        }

        public IAbstractResponse Response { get; set; }
    }
}