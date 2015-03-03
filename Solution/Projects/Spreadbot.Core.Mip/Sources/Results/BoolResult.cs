using Crocodev.Common;

namespace Spreadbot.Core.Mip
{
    public class BoolResult : IResponseResult
    {
        public BoolResult(bool value)
        {
            Value = value;
        }

        public bool Value { get; set; }

        public string GetDescription(string format)
        {
            return format.SafeFormat("Value", Value);
        }
    }
}