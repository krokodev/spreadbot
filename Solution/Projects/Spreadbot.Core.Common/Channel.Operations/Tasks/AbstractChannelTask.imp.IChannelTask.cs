// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Common
// AbstractChannelTask.imp.IChannelTask.cs
// romak_000, 2015-03-19 15:49

using Spreadbot.Core.Common.Channel.Operations.Args;
using Spreadbot.Core.Common.Channel.Operations.Methods;

namespace Spreadbot.Core.Common.Channel.Operations.Tasks
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