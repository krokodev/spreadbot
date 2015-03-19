// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Common
// IStore.cs
// romak_000, 2015-03-19 13:43

using System.Collections.Generic;
using Spreadbot.Core.Common.Channel.Operations.Tasks;
using Spreadbot.Core.Common.StoreOperations;

namespace Spreadbot.Core.Common.Store
{
    public interface IStore
    {
        IEnumerable<IChannelTask> GetChannelTasks();
        IEnumerable<IStoreTask> StoreTasks { get; }
        string Name { get; }
    }
}