using Spreadbot.Core.Channel.Ebay.Mip;
using Spreadbot.Core.System;

namespace Spreadbot.Core.Channel.Ebay
{
    // Code: EbayPublishTask
    public class EbayPublishTask : ChannelTask
    {
        public EbayPublishTask(FeedType feedType)
        {
            Channel = new EbayChannel();
            Args = new EbayPublishTaskArgs
            {
                Feed = new Feed(feedType)
            };
        }
    }
}