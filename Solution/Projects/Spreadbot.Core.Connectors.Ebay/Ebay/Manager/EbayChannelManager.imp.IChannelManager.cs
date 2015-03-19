// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// EbayChannelManager.imp.IChannelManager.cs
// romak_000, 2015-03-19 19:58

using Spreadbot.Core.Common.Channel.Operations.Tasks;


namespace Spreadbot.Core.Connectors.Ebay.Channel
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
        public override string Id { get { return ConstId; } }
    }
}