// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Common
// AbstractChannel.cs
// romak_000, 2015-03-19 13:43

using Spreadbot.Core.Common.Channel.Operations.Tasks;

namespace Spreadbot.Core.Common.Channel
{
    public abstract class AbstractChannel : IChannel
    {
        public abstract string Name { get; }

        public abstract void Publish(IChannelTask task);

        public abstract void ProceedTask(IChannelTask channelTask);
    }
}