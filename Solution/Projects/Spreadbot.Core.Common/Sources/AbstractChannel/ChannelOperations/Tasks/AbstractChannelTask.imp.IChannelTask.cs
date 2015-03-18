// !>> Core | AbstractChannelTask.imp.IChannelTask

namespace Spreadbot.Core.Common
{
    public abstract partial class AbstractChannelTask
    {
        // ===================================================================================== []
        // IChannelTask
        IChannel IChannelTask.ChannelRef
        {
            get { return ChannelRef; }
        }

        // --------------------------------------------------------[]
        public ChannelMethod ChannelMethod { get; set; }
        // --------------------------------------------------------[]
        public IChannelTaskArgs GetChannelArgs()
        {
            return (IChannelTaskArgs) AbstractArgs;
        }
    }
}