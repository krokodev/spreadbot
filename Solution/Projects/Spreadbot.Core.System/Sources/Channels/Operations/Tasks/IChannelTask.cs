using Spreadbot.Sdk.Common;

namespace Spreadbot.Core.System
{
    public interface IChannelTask : ITask
    {
        IChannel Channel { get; }
        ChannelMethod Method { get; }
    }
}