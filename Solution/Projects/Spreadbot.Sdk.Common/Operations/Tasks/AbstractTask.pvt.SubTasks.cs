// Spreadbot (c) 2015 Crocodev
// Spreadbot.Sdk.Common
// AbstractTask.pvt.SubTasks.cs
// romak_000, 2015-03-19 15:38

using System.Collections.Generic;
using System.Linq;
using Spreadbot.Sdk.Common.Exceptions;

// !>> Core | Sdk | AbstractTask.pvt.Subtasks

namespace Spreadbot.Sdk.Common.Operations.Tasks
{
    public abstract partial class AbstractTask
    {
        // ===================================================================================== []
        // SubTasks
        private List<AbstractTask> _subTasks = new List<AbstractTask>();
        // --------------------------------------------------------[]
        public AbstractTask DoAddSubTask(AbstractTask task)
        {
            _subTasks.Add(task);
            return this;
        }

        // ===================================================================================== []
        // StatusCode
        private TaskStatus DoCalcSuperTaskStatusCode()
        {
            var totalSubCount = SubTasks.Count();

            if (SubTasks.Any(t => t.GetStatusCode() == TaskStatus.Unknown))
            {
                return TaskStatus.Unknown;
            }

            if (SubTasks.Count(t => t.GetStatusCode() == TaskStatus.Todo) == totalSubCount)
            {
                return TaskStatus.Todo;
            }

            if (SubTasks.Count(t => t.GetStatusCode() == TaskStatus.Success) == totalSubCount)
            {
                return TaskStatus.Success;
            }

            if (SubTasks.Any(t => t.IsCritical && t.GetStatusCode() == TaskStatus.Fail) ||
                SubTasks.Count(t => t.GetStatusCode() == TaskStatus.Fail) == totalSubCount)
            {
                return TaskStatus.Fail;
            }

            if (SubTasks.Count(t => t.GetStatusCode() == TaskStatus.Todo) +
                SubTasks.Count(t => t.GetStatusCode() == TaskStatus.Inprocess) +
                SubTasks.Count(t => t.GetStatusCode() == TaskStatus.Success) +
                SubTasks.Count(t => t.GetStatusCode() == TaskStatus.Fail && !t.IsCritical)
                == totalSubCount)
            {
                return TaskStatus.Inprocess;
            }

            throw new SpreadbotException("Can't calculate Status Code");
        }
    }
}