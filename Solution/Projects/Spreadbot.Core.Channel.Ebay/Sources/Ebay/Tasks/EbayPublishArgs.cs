using Crocodev.Common;
using Spreadbot.Core.Channel.Ebay.Mip;
using Spreadbot.Core.Common;
using Spreadbot.Core.System;

namespace Spreadbot.Core.Channel.Ebay
{
    // Code: EbayPublishTaskArgs
    public class EbayPublishArgs : Args
    {
        public Feed Feed { get; set; }

        public override string Autoinfo
        {
            get { return "Feed:[{0}]".SafeFormat(Feed); }
        }
    }
}