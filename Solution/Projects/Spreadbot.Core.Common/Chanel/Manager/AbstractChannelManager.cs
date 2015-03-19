// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Common
// AbstractChannel.cs
// romak_000, 2015-03-19 15:49

using Spreadbot.Core.Common.Channel.Operations.Tasks;

namespace Spreadbot.Core.Common.Channel
{
    public abstract class AbstractChannelManager : IChannelManager
    {
        public abstract string Id { get; }

        public abstract void RunPublishTask(IChannelTask task);

        public abstract void ProceedTask(IChannelTask channelTask);
    }
}