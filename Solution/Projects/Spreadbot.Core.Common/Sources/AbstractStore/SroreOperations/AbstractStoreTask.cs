using Spreadbot.Sdk.Common;

// !>> Core | StoreTask

namespace Spreadbot.Core.Common
{
    public abstract class AbstractStoreTask : AbstractTask, IStoreTask
    {
        protected AbstractStoreTask(IStore store, string description)
        {
            Store = store;
            Description = description;
        }

        public override string Autoinfo
        {
            get
            {
                return string.Format(
                    "{2} {0}: {1}",
                    StatusCode,
                    Description,
                    Store.Name
                    );
            }
        }

        public IStore Store { get; set; }
    }
}