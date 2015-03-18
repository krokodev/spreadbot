using Nereal.Serialization;
// !>> Core | AbstractChannelTask.state

namespace Spreadbot.Core.Common
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