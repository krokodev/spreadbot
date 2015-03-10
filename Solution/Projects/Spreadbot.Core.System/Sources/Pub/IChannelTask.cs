using Spreadbot.Core.Common;

namespace Spreadbot.Core.System
{
    public interface IChannelTask
    {
        string Autoinfo { get; }
        IChannel Channel { get; }
        IChannelTaskArgs Args { get; }
        IResponse Response { get; set; }
    }
}