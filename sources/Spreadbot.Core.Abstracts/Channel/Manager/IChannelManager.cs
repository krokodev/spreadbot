// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Abstracts
// IChannelManager.cs
// Roman, 2015-04-07 2:57 PM

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