using Spreadbot.Core.Common;

namespace Spreadbot.Core.System
{
    public interface IChannelTask
    {
        IChannel Channel { get; }
        IChannelTaskArgs Args { get; }
        IResponse Response { get; set; }
    }
}