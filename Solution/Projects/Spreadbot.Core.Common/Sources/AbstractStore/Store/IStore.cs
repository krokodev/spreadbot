using System.Collections.Generic;

namespace Spreadbot.Core.Common
{
    public interface IStore
    {
        IEnumerable<IChannelTask> GetChannelTasks();
        IEnumerable<IStoreTask> StoreTasks { get; }
        string Name { get; }
    }
}
