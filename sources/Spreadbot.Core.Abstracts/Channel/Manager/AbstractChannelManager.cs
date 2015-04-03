// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Abstracts
// AbstractChannelManager.cs
// Roman, 2015-04-03 8:16 PM

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