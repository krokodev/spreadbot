using Spreadbot.Sdk.Common;

namespace Spreadbot.Core.Common
{
    public interface IChannelTask : ITask
    {
        IChannel ChannelRef { get; }
        ChannelMethod Method { get; }
        IChannelTaskArgs ChannelArgs { get; }
    }
}