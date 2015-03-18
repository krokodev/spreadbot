using Spreadbot.Sdk.Common;

// !>> Core | AbstractChannelTask

namespace Spreadbot.Core.Common
{
    public abstract partial class AbstractChannelTask : AbstractTask, IChannelTask
    {
        // --------------------------------------------------------[]
        protected AbstractChannelTask(IChannel channelRef, ChannelMethod channelMethod)
        {
            ChannelRef = channelRef;
            ChannelMethod = channelMethod;
        }

        // --------------------------------------------------------[]
        protected AbstractChannelTask()
            : this(null, ChannelMethod.Unknown)
        {
        }
    }
}