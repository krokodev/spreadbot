// Spreadbot (c) 2015 Crocodev
// Spreadbot.Sdk.Common
// BoolResponseResult.cs
// romak_000, 2015-03-25 15:25

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
            get { return string.Format( Template, "Value", Value ); }
        }
    }
}