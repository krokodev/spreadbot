// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Abstracts
// AbstractChannelManager.cs
// romak_000, 2015-03-26 19:42

using Spreadbot.Core.Abstracts.Channel.Operations.Tasks;

namespace Spreadbot.Core.Abstracts.Channel.Manager
{
    public abstract class AbstractChannelManager : IChannelManager
    {
        public abstract string Id { get; }

        public abstract void RunPublishTask( IChannelTask task );

        public abstract void ProceedTask( IChannelTask channelTask );
    }
}