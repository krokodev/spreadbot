// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Common
// IChannel.cs
// romak_000, 2015-03-19 15:49

using Spreadbot.Core.Common.Channel.Operations.Tasks;

namespace Spreadbot.Core.Common.Channel
{
    public interface IChannel
    {
        string Name { get; }
        void Publish(IChannelTask task);
        void ProceedTask(IChannelTask task);
    }
}