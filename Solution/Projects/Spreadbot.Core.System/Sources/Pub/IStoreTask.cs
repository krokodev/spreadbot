using System;

namespace Spreadbot.Core.System
{
    public interface IStoreTask
    {
        Guid ChannelId { get; }
    }
}