using System.Collections.Generic;

namespace Spreadbot.Core.System
{
    public interface IStore
    {
        IEnumerable<IChannelTask> Tasks { get; }
    }
}
