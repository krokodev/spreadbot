// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Common
// AbstractStoreTask.cs
// romak_000, 2015-03-19 15:37

using Nereal.Serialization;
using Spreadbot.Sdk.Common.Operations.Tasks;

namespace Spreadbot.Core.Common.Store.Operations
{
    public abstract class AbstractStoreTask : AbstractTask, IStoreTask
    {
        protected AbstractStoreTask(IStore storeRef, string description)
        {
            StoreRef = storeRef;
            Description = description;
        }

        protected AbstractStoreTask()
        {
        }

        public override string Autoinfo
        {
            get
            {
                return string.Format(
                    "Store {2} {0}: {1}",
                    GetStatusCode(),
                    Description,
                    StoreRef.Name
                    );
            }
        }

        [NotSerialize]
        // Code: AbstractStoreTask.StoreRef
        // Todo: Use Resolver and [Reference]
        public IStore StoreRef { get; set; }
    }
}