using Crocodev.Common;
using Spreadbot.Core.Channel.Ebay.Mip;
using Spreadbot.Core.Common;
using Spreadbot.Sdk.Common;

namespace Spreadbot.Core.Channel.Ebay
{
    public class EbayPublishArgs : AbstractArgs
    {
        public MipFeed MipFeed { get; set; }

        public override string Autoinfo
        {
            get { return "Feed:[{0}]".SafeFormat(MipFeed); }
        }
    }
}