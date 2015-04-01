// Spreadbot (c) 2015 Crocodev
// Spreadbot.Sdk.Common
// ResponseException.cs
// Roman, 2015-04-01 9:51 PM

using Spreadbot.Sdk.Common.Exceptions;

namespace Spreadbot.Sdk.Common.Operations.Responses
{
    public class ResponseException : SpreadbotException
    {
        public ResponseException( IAbstractResponse response )
            : base( "Response: ", response.ToString() ) {}
    }
}