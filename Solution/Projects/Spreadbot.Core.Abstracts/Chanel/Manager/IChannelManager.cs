// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Abstracts
// IChannelManager.cs
// romak_000, 2015-03-21 2:11

using Spreadbot.Core.Abstracts.Chanel.Operations.Tasks;

namespace Spreadbot.Core.Abstracts.Chanel.Manager
{
    public interface IChannelManager
    {
        string Id { get; }
        void RunPublishTask( IChannelTask task );
        void ProceedTask( IChannelTask task );
    }
}