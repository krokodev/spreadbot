// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// EbayChannelManager.imp.IChannelManager.cs
// romak_000, 2015-03-26 19:42

using Spreadbot.Core.Abstracts.Chanel.Operations.Tasks;
using Spreadbot.Core.Channels.Ebay.Operations.Tasks;

namespace Spreadbot.Core.Channels.Ebay.Manager
{
    public partial class EbayChannelManager
    {
        // --------------------------------------------------------[]
        public override void RunPublishTask( IChannelTask task )
        {
            DoRunPublishTask( ( EbayPublishTask ) task );
        }

        // --------------------------------------------------------[]
        public override void ProceedTask( IChannelTask channelTask )
        {
            DoProceedTask( ( EbayPublishTask ) channelTask );
        }

        // --------------------------------------------------------[]
        public override string Id
        {
            get { return ConstId; }
        }
    }
}