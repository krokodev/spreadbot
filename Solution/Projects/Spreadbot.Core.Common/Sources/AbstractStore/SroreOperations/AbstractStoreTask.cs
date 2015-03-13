using Spreadbot.Sdk.Common;

// !>> Core | StoreTask

namespace Spreadbot.Core.Common
{
    public abstract class AbstractStoreTask : AbstractTask, IStoreTask
    {
        protected AbstractStoreTask(string description)
        {
            Description = description;
        }

        public override TaskStatus StatusCode
        {
            get { return TaskStatus.Unknown; }
        }
    }
}