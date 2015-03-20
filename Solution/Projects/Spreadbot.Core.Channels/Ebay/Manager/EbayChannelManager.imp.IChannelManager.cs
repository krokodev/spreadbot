// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// EbayChannelManager.imp.IChannelManager.cs
// romak_000, 2015-03-20 13:56

using Spreadbot.Core.Abstracts.Chanel.Operations.Tasks;

namespace Spreadbot.Core.Channels.Ebay.Manager
{
    public partial class EbayChannelManager
    {
        // --------------------------------------------------------[]
        public override void RunPublishTask( IChannelTask task )
        {
            DoRunPublishTask( task );
        }

        // --------------------------------------------------------[]
        public override void ProceedTask( IChannelTask channelTask )
        {
            DoProceedTask( channelTask );
        }

        // --------------------------------------------------------[]
        public override string Id
        {
            get { return ConstId; }
        }
    }
}