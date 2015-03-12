using Spreadbot.Core.Common;

namespace Spreadbot.Core.System
{
    public class StoreTask : Task, IStoreTask
    {
        public StoreTask(string description)
        {
            Description = description;
        }
        // Code: StoreTask : Autoinfo

        public override TaskStatus StatusCode
        {
            get { return TaskStatus.Unknown; }
        }
    }
}