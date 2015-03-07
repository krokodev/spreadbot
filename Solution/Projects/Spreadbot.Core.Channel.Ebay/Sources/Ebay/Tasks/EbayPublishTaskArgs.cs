using Spreadbot.Core.Channel.Ebay.Mip;
using Spreadbot.Core.System;

namespace Spreadbot.Core.Channel.Ebay
{
    public class EbayPublishTaskArgs : IChannelTaskArgs
    {
        public Feed Feed { get; set; }
    }
}