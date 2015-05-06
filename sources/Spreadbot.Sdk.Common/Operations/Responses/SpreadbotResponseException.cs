// Spreadbot (c) 2015 Krokodev
// Spreadbot.Sdk.Common
// SpreadbotResponseException.cs

using Krokodev.Common.Extensions;
using Spreadbot.Sdk.Common.Exceptions;
using Spreadbot.Sdk.Common.Krokodev.Common;

namespace Spreadbot.Sdk.Common.Operations.Responses
{
    public class SpreadbotResponseException : SpreadbotException, ISpreadbotDetaledException
    {
        public string ResponseDetails { get; set; }

        public SpreadbotResponseException( IAbstractResponse response )
            : base( "{0} failed, see field 'ResponseDetails'",  response.GetType())
        {
            ResponseDetails = "[Response]\n{0}".SafeFormat( response.ToYamlString() );
        }

        public string GetDetails()
        {
            return ResponseDetails;
        }
    }
}