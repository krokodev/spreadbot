using Spreadbot.Core.Common;

namespace Spreadbot.Core.System
{
    public interface IChannelTask : ITask
    {
        IChannel Channel { get; }
        ChannelOperation Operation { get; }
    }
}