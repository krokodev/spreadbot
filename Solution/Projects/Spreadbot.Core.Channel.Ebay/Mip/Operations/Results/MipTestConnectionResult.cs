// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Connectors.Ebay
// MipTestConnectionResult.cs
// romak_000, 2015-03-19 14:02

using Crocodev.Common;

namespace Spreadbot.Core.Channel.Ebay.Mip.Operations.Results
{
    public class MipTestConnectionResult : AbstractMipResponseResult
    {
        public MipTestConnectionResult(bool value)
        {
            Value = value;
        }

        public bool Value { get; set; }

        public override string Autoinfo
        {
            get { return Template.SafeFormat("Value", Value); }
        }
    }
}