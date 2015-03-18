using Spreadbot.Sdk.Common;

namespace Spreadbot.Core.Common
{
    public interface IStoreTask: IHierarchicalTask
    {
        IStore StoreRef { get; set; }
    }
}
