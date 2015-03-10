namespace Spreadbot.Core.System
{
    public abstract class ChannelTaskArgs : IChannelTaskArgs
    {
        public override string ToString()
        {
            return Autoinfo;
        }
        public abstract string Autoinfo { get; }
    }
}