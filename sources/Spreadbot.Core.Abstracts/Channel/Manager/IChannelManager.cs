// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Abstracts
// IChannelManager.cs
// romak_000, 2015-03-26 19:42

using Spreadbot.Core.Abstracts.Channel.Operations.Tasks;

namespace Spreadbot.Core.Abstracts.Channel.Manager
{
    public interface IChannelManager
    {
        string Id { get; }
        void RunPublishTask( IChannelTask task );
        void ProceedTask( IChannelTask task );
    }
}