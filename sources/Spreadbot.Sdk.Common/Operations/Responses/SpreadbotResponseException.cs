// Spreadbot (c) 2015 Krokodev
// Spreadbot.Sdk.Common
// SpreadbotResponseException.cs

using Krokodev.Common.Extensions;
using Spreadbot.Sdk.Common.Crocodev.Common;
using Spreadbot.Sdk.Common.Exceptions;

namespace Spreadbot.Sdk.Common.Operations.Responses
{
    public class SpreadbotResponseException : SpreadbotException, ISpreadbotDetaledException
    {
        public string ResponseDetails { get; set; }

        public SpreadbotResponseException( IAbstractResponse response )
            : base( "ISpreadbotDetaledException, see details." )
        {
            ResponseDetails = "[Response]\n{0}".SafeFormat( response.ToYamlString() );
        }

        public string GetDetails()
        {
            return ResponseDetails;
        }
    }
}