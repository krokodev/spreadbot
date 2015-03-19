// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Connectors.Ebay
// EbayPublishArgs.cs
// romak_000, 2015-03-19 15:49

using Crocodev.Common.Extensions;
using Spreadbot.Core.Common.Channel.Operations.Args;
using Spreadbot.Core.Connectors.Ebay.Mip.Feed;
using Spreadbot.Sdk.Common.Operations.Args;

namespace Spreadbot.Core.Connectors.Ebay.Channel.Operations.Args
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