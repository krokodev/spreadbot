using Spreadbot.Sdk.Common;

namespace Spreadbot.Core.Common
{
    public interface IStoreTask: ITask
    {
        IStore StoreRef { get; set; }
    }
}
