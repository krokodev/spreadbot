// Spreadbot (c) 2015 Crocodev
// Spreadbot.Sdk.Common
// AbstractTask.imp.ITask.cs
// romak_000, 2015-03-19 15:49

using System.Collections.Generic;
using Spreadbot.Sdk.Common.Operations.Args;
using Spreadbot.Sdk.Common.Operations.Responses;

// !>> Core | Sdk | AbstractTask.imp.ITask

namespace Spreadbot.Sdk.Common.Operations.Tasks
{
    public abstract partial class AbstractTask
    {
        // ===================================================================================== []
        // Implicit
        public abstract string GetAutoinfo();
        // --------------------------------------------------------[]
        public string Description { get; set; }
        // --------------------------------------------------------[]
        public abstract TaskStatus GetStatusCode();

        // ===================================================================================== []
        // Explicit
        ITaskArgs ITask.Args
        {
            get { return AbstractArgs; }
        }

        // --------------------------------------------------------[]
        IResponse ITask.Response { get; set; }
        // --------------------------------------------------------[]
        IEnumerable<ITask> ITask.SubTasks
        {
            get { return SubTasks; }
        }
    }
}