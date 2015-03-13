using Crocodev.Common;
using Spreadbot.Core.Channel.Ebay.Mip;
using Spreadbot.Core.Common;
using Spreadbot.Core.System;
using Spreadbot.Sdk.Common;

namespace Spreadbot.Core.Channel.Ebay
{
    public class EbayPublishArgs : AbstractArgs, IChannelTaskArgs
    {
        public MipFeed MipFeed { get; set; }

        public override string Autoinfo
        {
            get { return "Feed:[{0}]".SafeFormat(MipFeed); }
        }
    }
}