// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Common
// AbstractStoreTask.cs
// romak_000, 2015-03-19 15:53

using Nereal.Serialization;
using Spreadbot.Sdk.Common.Operations.Tasks;

namespace Spreadbot.Core.Common.Store.Operations
{
    public abstract class AbstractStoreTask : AbstractTask, IStoreTask
    {
        protected AbstractStoreTask(string storeId, string description)
        {
            StoreId = storeId;
            Description = description;
        }

        protected AbstractStoreTask()
        {
        }

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