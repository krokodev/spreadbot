using Spreadbot.Core.Common;

namespace Spreadbot.Core.System
{
    public class ChannelTask : IChannelTask
    {
        public ChannelTask()
        {
        }
        
        public ChannelTask(IChannel channel, IChannelTaskArgs args)
        {
            Channel = channel;
            Args = args;
        }

        public IChannel Channel { get; set; }

        public IChannelTaskArgs Args { get; set; }

        public IResponse Response  { get; set; }
    }
}