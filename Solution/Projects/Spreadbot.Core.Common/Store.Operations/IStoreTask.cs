// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Common
// IStoreTask.cs
// romak_000, 2015-03-19 15:37

using Spreadbot.Sdk.Common.Operations.Tasks;

namespace Spreadbot.Core.Common.Store.Operations
{
    public interface IStoreTask : ITask
    {
        IStore StoreRef { get; set; }
    }
}