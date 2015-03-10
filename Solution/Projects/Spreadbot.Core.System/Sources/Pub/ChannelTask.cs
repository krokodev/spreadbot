using Crocodev.Common;
using Spreadbot.Core.Common;

namespace Spreadbot.Core.System
{
    // Code: ChannelTask
    public class ChannelTask : IChannelTask
    {
        protected ChannelTask()
        {
        }

        public virtual string Description
        {
            get { return "Channel:[{0}] Args:[{1}] Response:[{2}]".SafeFormat(Channel.Name, Args, Response==null?"no":Response.Description); }
        }

        public override string ToString()
        {
            return Description;
        }

        public IChannel Channel { get; set; }

        public IChannelTaskArgs Args { get; set; }

        public IResponse Response  { get; set; }
    }
}