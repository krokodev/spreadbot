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

        public override TaskStatusCode StatusCode
        {
            get { return TaskStatusCode.Unknown; }
        }
    }
}