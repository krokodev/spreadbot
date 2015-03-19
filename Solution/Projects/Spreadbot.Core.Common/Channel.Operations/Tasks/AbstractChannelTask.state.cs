// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Common
// AbstractChannelTask.state.cs
// romak_000, 2015-03-19 15:49

using Nereal.Serialization;

// !>> Core | AbstractChannelTask.state

namespace Spreadbot.Core.Common.Channel.Operations.Tasks
{
    public abstract partial class AbstractChannelTask
    {
        // ===================================================================================== []
        // Protected
        [NotSerialize]
        // Code: AbstractChannelTask.ChannelRef
        protected IChannel ChannelRef { get; set; }
    }
}