namespace Spreadbot.Core.System
{
    public abstract class ChannelTaskArgs : IChannelTaskArgs
    {
        public override string ToString()
        {
            return Description;
        }
        public abstract string Description { get; }
    }
}