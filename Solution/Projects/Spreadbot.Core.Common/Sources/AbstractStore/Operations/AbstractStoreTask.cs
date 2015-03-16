// !>> Core | AbstractStoreTask
using Spreadbot.Sdk.Common;
using Nereal.Serialization;

namespace Spreadbot.Core.Common
{
    public abstract class AbstractStoreTask : AbstractTask, IStoreTask
    {
        protected AbstractStoreTask(IStore store, string description)
        {
            Store = store;
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
                    "{2} {0}: {1}",
                    StatusCode,
                    Description,
                    "Store.Name"
                    );
            }
        }

        [NotSerialize]
        public IStore Store { get; set; }
    }
}