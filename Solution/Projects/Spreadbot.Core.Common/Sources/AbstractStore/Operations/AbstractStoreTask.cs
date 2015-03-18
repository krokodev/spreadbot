// !>> Core | AbstractStoreTask
using Spreadbot.Sdk.Common;
using Nereal.Serialization;

namespace Spreadbot.Core.Common
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