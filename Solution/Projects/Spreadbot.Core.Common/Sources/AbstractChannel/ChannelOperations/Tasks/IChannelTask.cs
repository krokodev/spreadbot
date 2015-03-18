using Spreadbot.Sdk.Common;

namespace Spreadbot.Core.Common
{
    public interface IChannelTask : IHierarchicalTask
    {
        IChannel ChannelRef { get; }
        ChannelMethod ChannelMethod { get; }
        IChannelTaskArgs GetChannelArgs();
    }
}