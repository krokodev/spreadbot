using System.Collections.Generic;

// !>> Core | Sdk | AbstractTask.imp.ITask

namespace Spreadbot.Sdk.Common
{
    public abstract partial class AbstractTask
    {
        // ===================================================================================== []
        // Implicit
        // Todo: Ref: Rename to GetAutoInfo()
        public abstract string Autoinfo { get; }
        // --------------------------------------------------------[]
        public string Description { get; set; }
        // --------------------------------------------------------[]
        public abstract TaskStatus GetStatusCode();

        // ===================================================================================== []
        // Explicit
        ITaskArgs IHierarchicalTask.Args
        {
            get { return AbstractArgs; }
        }

        // --------------------------------------------------------[]
        IResponse IHierarchicalTask.Response { get; set; }
        // --------------------------------------------------------[]
        IEnumerable<IHierarchicalTask> IHierarchicalTask.SubTasks
        {
            get { return SubTasks; }
        }
    }
}