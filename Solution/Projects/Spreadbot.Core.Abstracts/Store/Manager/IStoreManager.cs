// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Abstracts
// IStoreManager.cs
// romak_000, 2015-03-25 15:24

using System.Collections.Generic;
using Spreadbot.Core.Abstracts.Chanel.Operations.Tasks;
using Spreadbot.Core.Abstracts.Store.Operations.Tasks;

namespace Spreadbot.Core.Abstracts.Store.Manager
{
    public interface IStoreManager
    {
        IEnumerable< IChannelTask > GetChannelTasks();
        IEnumerable< IStoreTask > StoreTasks { get; }
        string Id { get; }
    }
}