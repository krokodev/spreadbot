// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Abstracts
// IChannelManager.cs

using Spreadbot.Core.Abstracts.Channel.Operations.Tasks;

namespace Spreadbot.Core.Abstracts.Channel.Manager
{
    public interface IChannelManager
    {
        string Id { get; }
        void RunSubmissionTask( IChannelTask task );
        void ProceedTask( IChannelTask task );
    }
}