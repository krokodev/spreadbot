// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Abstracts
// IChannelAdapter.cs

using Spreadbot.Core.Abstracts.Channel.Operations.Tasks;

namespace Spreadbot.Core.Abstracts.Channel.Adapter
{
    public interface IChannelAdapter
    {
        string Id { get; }
        void RunSubmissionTask( IChannelTask task );
        void ProceedTask( IChannelTask task );
    }
}