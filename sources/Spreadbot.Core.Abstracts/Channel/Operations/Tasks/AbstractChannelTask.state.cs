// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Abstracts
// AbstractChannelTask.state.cs
// romak_000, 2015-03-26 19:42

using YamlDotNet.Serialization;

namespace Spreadbot.Core.Abstracts.Channel.Operations.Tasks
{
    public abstract partial class AbstractChannelTask
    {
        [YamlMember(Order = 9)]
        public string ChannelId { get; set; }
    }
}