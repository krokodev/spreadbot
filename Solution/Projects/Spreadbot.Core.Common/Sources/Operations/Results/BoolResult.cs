using Crocodev.Common;

namespace Spreadbot.Core.Common

{
    public class BoolResult : ResponseResult
    {
        public BoolResult(bool value)
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