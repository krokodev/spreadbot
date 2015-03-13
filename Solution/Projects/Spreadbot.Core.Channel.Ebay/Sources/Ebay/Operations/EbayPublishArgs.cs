using Crocodev.Common;
using Spreadbot.Core.Channel.Ebay.Mip;
using Spreadbot.Core.Common;
using Spreadbot.Sdk.Common;

namespace Spreadbot.Core.Channel.Ebay
{
    public class EbayPublishArgs : AbstractArgs, IChannelTaskArgs
    {
        public MipFeed Feed { get; set; }

        public override string Autoinfo
        {
            get { return "Feed:[{0}]".SafeFormat(Feed); }
        }
    }
}