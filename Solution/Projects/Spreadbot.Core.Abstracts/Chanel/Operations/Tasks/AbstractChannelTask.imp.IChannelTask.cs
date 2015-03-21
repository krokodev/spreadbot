// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Abstracts
// AbstractChannelTask.imp.IChannelTask.cs
// romak_000, 2015-03-21 2:11

using Spreadbot.Core.Abstracts.Chanel.Operations.Methods;

namespace Spreadbot.Core.Abstracts.Chanel.Operations.Tasks
{
    public abstract partial class AbstractChannelTask
    {
        // ===================================================================================== []
        // IChannelTask
        string IChannelTask.ChannelId
        {
            get { return ChannelId; }
        }

        // --------------------------------------------------------[]
        public ChannelMethod ChannelMethod { get; set; }
    }
}