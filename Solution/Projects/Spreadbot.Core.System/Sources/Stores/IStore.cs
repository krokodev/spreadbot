using System.Collections.Generic;
using Spreadbot.Sdk.Common;

namespace Spreadbot.Core.System
{
    public interface IStore
    {
        IEnumerable<ITask> Tasks { get; }
        IEnumerable<IChannelTask> ChannelTasks { get; }
        IEnumerable<IStoreTask> StoreTasks { get; }
    }
}
