using Spreadbot.Sdk.Common;

namespace Spreadbot.Core.Common
{
    public interface IChannelTask : ITask
    {
        IChannel Channel { get; }
        ChannelMethod Method { get; }
        IChannelTaskArgs ChannelArgs { get; }
    }
}