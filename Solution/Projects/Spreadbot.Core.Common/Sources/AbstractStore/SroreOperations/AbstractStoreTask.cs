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

        public override string Autoinfo
        {
            get
            {
                return string.Format(
                    "{0}: {1}",
                    StatusCode,
                    Description
                    );
            }
        }
    }
}