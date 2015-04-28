// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Abstracts
// AbstractChannelTask.state.cs

using YamlDotNet.Serialization;

namespace Spreadbot.Core.Abstracts.Channel.Operations.Tasks
{
    public abstract partial class AbstractChannelTask
    {
        [YamlMember( Order = 9 )]
        public string ChannelId { get; set; }
    }
}