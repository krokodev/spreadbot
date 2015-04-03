// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Abstracts
// AbstractChannelTask.state.cs
// Roman, 2015-04-03 1:44 PM

using YamlDotNet.Serialization;

namespace Spreadbot.Core.Abstracts.Channel.Operations.Tasks
{
    public abstract partial class AbstractChannelTask
    {
        [YamlMember( Order = 9 )]
        public string ChannelId { get; set; }
    }
}