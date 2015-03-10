using System;
using Spreadbot.Core.Common;

namespace Spreadbot.Core.System
{
    public class StoreTask : Task, IStoreTask
    {
        public StoreTask(string description)
        {
            Description = description;
        }
    }
}