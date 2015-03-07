using Crocodev.Common;
using Spreadbot.Core.Common;

namespace Spreadbot.Core.Channel.Ebay.Mip
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