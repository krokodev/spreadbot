using System.Collections.Generic;
using Spreadbot.Sdk.Common;

namespace Spreadbot.Core.Common
{
    public interface IStore
    {
        IEnumerable<IChannelTask> ChannelTasks { get; }
        IEnumerable<IStoreTask> StoreTasks { get; }
        string Name { get; }
    }
}
