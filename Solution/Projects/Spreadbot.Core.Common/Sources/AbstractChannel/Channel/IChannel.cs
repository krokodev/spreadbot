namespace Spreadbot.Core.Common
{
    public interface IChannel
    {
        string Name { get; }
        void Publish(IChannelTask task);
        void ProceedTask(IChannelTask task);
    }
}
