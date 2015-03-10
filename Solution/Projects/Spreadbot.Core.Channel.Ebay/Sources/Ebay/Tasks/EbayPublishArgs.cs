using Crocodev.Common;
using Spreadbot.Core.Channel.Ebay.Mip;
using Spreadbot.Core.Common;

namespace Spreadbot.Core.Channel.Ebay
{
    public class EbayPublishArgs : Args
    {
        public Feed Feed { get; set; }

        public override string Autoinfo
        {
            get { return "Feed:[{0}]".SafeFormat(Feed); }
        }
    }
}