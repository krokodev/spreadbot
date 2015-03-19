// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Common
// AbstractChannelTask.state.cs
// romak_000, 2015-03-19 17:24

using Nereal.Serialization;

namespace Spreadbot.Core.Common.Channel.Operations.Tasks
{
    public abstract partial class AbstractChannelTask
    {
        // ===================================================================================== []
        // Protected
        [Serialize]
        protected string ChannelId { get; set; }
    }
}