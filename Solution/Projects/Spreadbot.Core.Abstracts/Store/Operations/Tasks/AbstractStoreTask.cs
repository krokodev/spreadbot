// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Abstracts
// AbstractStoreTask.cs
// romak_000, 2015-03-21 0:58

using Spreadbot.Sdk.Common.Operations.Tasks;

namespace Spreadbot.Core.Abstracts.Store.Operations.Tasks
{
    public abstract class AbstractStoreTask : AbstractTask, IStoreTask
    {
        public override string GetAutoinfo()
        {
            return string.Format(
                "Store {2} {0}: {1}",
                GetStatusCode(),
                Description,
                StoreId
                );
        }

        public string StoreId { get; set; }
    }
}