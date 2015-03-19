// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Common
// AbstractChannelTask.cs
// romak_000, 2015-03-19 15:49

using Spreadbot.Core.Common.Channel.Operations.Methods;
using Spreadbot.Sdk.Common.Operations.Tasks;

// !>> Core | AbstractChannelTask

namespace Spreadbot.Core.Common.Channel.Operations.Tasks
{
    public abstract partial class AbstractChannelTask : AbstractTask, IChannelTask
    {
        // --------------------------------------------------------[]
        protected AbstractChannelTask(string channelId, ChannelMethod channelMethod)
        {
            ChannelId = channelId;
            ChannelMethod = channelMethod;
        }

        // --------------------------------------------------------[]
        protected AbstractChannelTask()
            : this(null, ChannelMethod.Unknown)
        {
        }
    }
}