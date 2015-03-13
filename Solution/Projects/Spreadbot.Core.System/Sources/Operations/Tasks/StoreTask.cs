using Spreadbot.Sdk.Common;

// !>> Core | StoreTask

namespace Spreadbot.Core.System
{
    public class StoreTask : Task, IStoreTask
    {
        public StoreTask(string description)
        {
            Description = description;
        }

        public override TaskStatus StatusCode
        {
            get { return TaskStatus.Unknown; }
        }
    }
}