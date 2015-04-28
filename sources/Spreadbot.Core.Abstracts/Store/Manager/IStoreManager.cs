// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Abstracts
// IStoreManager.cs

using System.Collections.Generic;
using Spreadbot.Core.Abstracts.Channel.Operations.Tasks;
using Spreadbot.Core.Abstracts.Store.Operations.Tasks;

namespace Spreadbot.Core.Abstracts.Store.Manager
{
    public interface IStoreManager
    {
        IEnumerable< IChannelTask > GetChannelTasks();
        IEnumerable< IStoreTask > StoreTasks { get; }
        string Id { get; }
        IStoreTask CreateTask( StoreTaskType taskType );
    }
}