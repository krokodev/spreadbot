namespace Spreadbot.Core.Common
{
    public abstract class AbstractChannel: IChannel
    {
        public abstract string Name { get; }

        public abstract IChannelResponse Publish(IChannelTaskArgs taskArgs);

        public abstract void ProceedTask(IChannelTask channelTask);
    }
}