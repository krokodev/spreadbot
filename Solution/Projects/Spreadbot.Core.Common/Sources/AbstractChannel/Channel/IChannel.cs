namespace Spreadbot.Core.Common
{
    public interface IChannel
    {
        string Name { get; }
        IChannelResponse Publish(IChannelTaskArgs taskArgs);
        void ProceedTask(IChannelTask channelTask);
    }
}
