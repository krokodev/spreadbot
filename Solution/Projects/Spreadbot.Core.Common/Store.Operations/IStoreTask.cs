// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Common
// IStoreTask.cs
// romak_000, 2015-03-19 13:43

using Spreadbot.Core.Common.Store;
using Spreadbot.Sdk.Common.Operations.Tasks;

namespace Spreadbot.Core.Common.StoreOperations
{
    public interface IStoreTask : ITask
    {
        IStore StoreRef { get; set; }
    }
}