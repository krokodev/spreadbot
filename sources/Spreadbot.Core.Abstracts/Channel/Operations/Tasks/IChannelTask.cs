// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Abstracts
// IChannelTask.cs
// Roman, 2015-03-31 1:26 PM

using Spreadbot.Core.Abstracts.Channel.Operations.Methods;
using Spreadbot.Sdk.Common.Operations.Tasks;

namespace Spreadbot.Core.Abstracts.Channel.Operations.Tasks
{
    public interface IChannelTask : IAbstractTask
    {
        string ChannelId { get; }
        ChannelMethod ChannelMethod { get; }
    }
}