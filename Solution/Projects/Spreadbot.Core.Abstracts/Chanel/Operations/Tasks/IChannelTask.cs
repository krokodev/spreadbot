// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Abstracts
// IChannelTask.cs
// romak_000, 2015-03-20 13:56

using Spreadbot.Core.Abstracts.Chanel.Operations.Args;
using Spreadbot.Core.Abstracts.Chanel.Operations.Methods;
using Spreadbot.Sdk.Common.Operations.Tasks;

namespace Spreadbot.Core.Abstracts.Chanel.Operations.Tasks
{
    public interface IChannelTask : ITask
    {
        string ChannelId { get; }
        ChannelMethod ChannelMethod { get; }
        IChannelTaskArgs GetChannelArgs();
    }
}