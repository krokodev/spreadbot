using Spreadbot.Core.Common;

namespace Spreadbot.Core.System
{
    public interface IStore
    {
        IStoreTask GetTaskForChannel(IChannel channel);
    }
}
