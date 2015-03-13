using Crocodev.Common;

namespace Spreadbot.Sdk.Common
{
    public class BoolResponseResult : AbstractResponseResult
    {
        public BoolResponseResult(bool value)
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