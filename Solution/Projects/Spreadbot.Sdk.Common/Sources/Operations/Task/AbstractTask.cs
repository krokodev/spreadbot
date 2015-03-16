using System;
using System.Collections.Generic;
using System.Linq;
using Crocodev.Common;
using Crocodev.Common.Identifier;

// !>> Core | Sdk | Task

namespace Spreadbot.Sdk.Common
{
    public abstract class AbstractTask : Identifiable<AbstractTask, int>, ITask
    {
        // ===================================================================================== []
        // Ctor
        protected AbstractTask()
        {
            IsCritical = true;
        }

        // ===================================================================================== []
        // Tasks
        private List<AbstractTask> _subTasks = new List<AbstractTask>();
        // --------------------------------------------------------[]
        public AbstractTask AddSubTask(AbstractTask task)
        {
            _subTasks.Add(task);
            return this;
        }

        // --------------------------------------------------------[]
        public List<AbstractTask> SubTasks
        {
            get { return _subTasks; }
            set { _subTasks=value; }
        }

        // ===================================================================================== []
        // Properties
        private static int _totalNum;
        public readonly Identifier Id = (Identifier) (++_totalNum);
        public readonly DateTime CreationTime = DateTime.Now;
        public abstract TaskStatus StatusCode { get; }
        public bool IsCritical { get; set; }
        public virtual AbstractArgs Args { get; protected set; }

        // ===================================================================================== []
        // ITask
        public virtual string Autoinfo
        {
            get { return "Args:[{0}] Response:[{1}]".SafeFormat(Args, Response == null ? "no" : Response.ToString()); }
        }

        // --------------------------------------------------------[]
        ITaskArgs ITask.Args { get { return Args; } }
        // --------------------------------------------------------[]
        public virtual IResponse Response { get; set; }
        // --------------------------------------------------------[]
        public string Description { get; set; }
        // --------------------------------------------------------[]
        IEnumerable<ITask> ITask.SubTasks
        {
            get { return _subTasks; }
        }

        // ===================================================================================== []
        // Object
        public override string ToString()
        {
            return Autoinfo;
        }

        // ===================================================================================== []
        // Utils
        protected TaskStatus CalcSuperTaskStatusCode()
        {
            var totalSubCount = SubTasks.Count();

            if (SubTasks.Any(t => t.StatusCode == TaskStatus.Unknown))
            {
                return TaskStatus.Unknown;
            }

            if (SubTasks.Count(t => t.StatusCode == TaskStatus.Todo) == totalSubCount)
            {
                return TaskStatus.Todo;
            }

            if (SubTasks.Count(t => t.StatusCode == TaskStatus.Success) == totalSubCount)
            {
                return TaskStatus.Success;
            }

            if (SubTasks.Any(t => t.IsCritical && t.StatusCode == TaskStatus.Fail) ||
                SubTasks.Count(t => t.StatusCode == TaskStatus.Fail) == totalSubCount)
            {
                return TaskStatus.Fail;
            }

            if (SubTasks.Count(t => t.StatusCode == TaskStatus.Todo) +
                SubTasks.Count(t => t.StatusCode == TaskStatus.Inprocess) +
                SubTasks.Count(t => t.StatusCode == TaskStatus.Success) +
                SubTasks.Count(t => t.StatusCode == TaskStatus.Fail && !t.IsCritical)
                == totalSubCount)
            {
                return TaskStatus.Inprocess;
            }

            throw new SpreadbotException("Can't calculate Status Code");
        }
    }
}