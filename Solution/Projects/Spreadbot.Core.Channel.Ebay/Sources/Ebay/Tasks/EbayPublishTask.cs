using Spreadbot.Core.Channel.Ebay.Mip;
using Spreadbot.Core.System;

namespace Spreadbot.Core.Channel.Ebay
{
    public sealed class EbayPublishTask : ChannelTask
    {
        public EbayPublishTask(FeedType feedType)
            :base(ChannelMethod.Publish)
        {
            Channel = new EbayChannel();
            Args = new EbayPublishArgs
            {
                Feed = new Feed(feedType)
            };
        }
    }
}