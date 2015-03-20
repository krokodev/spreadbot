// Spreadbot (c) 2015 Crocodev
// Spreadbot.Sdk.Common
// BoolResponseResult.cs
// romak_000, 2015-03-20 13:57

using Crocodev.Common.Extensions;

namespace Spreadbot.Sdk.Common.Operations.ResponseResults
{
    public class BoolResponseResult : AbstractResponseResult
    {
        public BoolResponseResult( bool value )
        {
            Value = value;
        }

        public bool Value { get; set; }

        public override string Autoinfo
        {
            get { return Template.SafeFormat( "Value", Value ); }
        }
    }
}