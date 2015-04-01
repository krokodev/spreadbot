// Spreadbot (c) 2015 Crocodev
// Spreadbot.Sdk.Common
// ResponseException.cs
// Roman, 2015-04-01 4:59 PM

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