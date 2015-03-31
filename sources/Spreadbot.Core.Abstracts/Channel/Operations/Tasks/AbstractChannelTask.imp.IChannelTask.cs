// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Abstracts
// AbstractChannelTask.imp.IChannelTask.cs
// romak_000, 2015-03-26 20:13

using Spreadbot.Core.Abstracts.Channel.Operations.Methods;
using YamlDotNet.Serialization;

namespace Spreadbot.Core.Abstracts.Channel.Operations.Tasks
{
    public abstract partial class AbstractChannelTask
    {
        [YamlMember( Order = 20 )]
        string IChannelTask.ChannelId
        {
            get { return ChannelId; }
        }

        [YamlMember( Order = 21 )]
        public ChannelMethod ChannelMethod { get; set; }
    }
}