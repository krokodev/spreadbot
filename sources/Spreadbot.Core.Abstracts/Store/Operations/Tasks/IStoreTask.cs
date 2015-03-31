// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Abstracts
// IStoreTask.cs
// romak_000, 2015-03-26 19:42

using Spreadbot.Sdk.Common.Operations.Tasks;

namespace Spreadbot.Core.Abstracts.Store.Operations.Tasks
{
    public interface IStoreTask : IAbstractTask
    {
        string StoreId { get; set; }
    }
}