// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Connectors.Ebay
// EbayPublishArgs.cs
// romak_000, 2015-03-19 14:02

using Crocodev.Common;
using Spreadbot.Core.Channel.Ebay.Mip.Feed;
using Spreadbot.Core.Common.Channel.Operations.Args;
using Spreadbot.Sdk.Common.Operations.Args;

namespace Spreadbot.Core.Channel.Ebay.Channel.Operations.Args
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