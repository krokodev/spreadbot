using Crocodev.Common;

namespace Spreadbot.Core.Channel.Ebay.Mip
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