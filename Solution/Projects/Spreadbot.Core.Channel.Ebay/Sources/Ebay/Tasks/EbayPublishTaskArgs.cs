using Crocodev.Common;
using Spreadbot.Core.Channel.Ebay.Mip;
using Spreadbot.Core.System;

namespace Spreadbot.Core.Channel.Ebay
{
    // Code: EbayPublishTaskArgs
    public class EbayPublishTaskArgs : ChannelTaskArgs
    {
        public Feed Feed { get; set; }

        public override string Autoinfo
        {
            get { return "Feed:[{0}]".SafeFormat(Feed); }
        }
    }
}