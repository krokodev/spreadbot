namespace Spreadbot.Core.Common
{
    public abstract class AbstractChannel: IChannel
    {
        public abstract string Name { get; }

        public abstract void Publish(IChannelTask task);

        public abstract void ProceedTask(IChannelTask channelTask);
    }
}