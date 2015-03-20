// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// EbayPublishArgs.cs
// romak_000, 2015-03-20 13:56

using Crocodev.Common.Extensions;
using Spreadbot.Core.Abstracts.Chanel.Operations.Args;
using Spreadbot.Core.Channels.Ebay.Mip.Feed;
using Spreadbot.Sdk.Common.Operations.Args;

namespace Spreadbot.Core.Channels.Ebay.Operations.Args
{
    public class EbayPublishArgs : AbstractArgs, IChannelTaskArgs
    {
        public MipFeed Feed { get; set; }

        public override string Autoinfo
        {
            get { return "Feed:[{0}]".SafeFormat( Feed ); }
        }
    }
}