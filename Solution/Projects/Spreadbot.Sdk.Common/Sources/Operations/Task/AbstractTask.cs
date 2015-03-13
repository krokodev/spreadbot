using System;
using System.Collections.Generic;
using Crocodev.Common;
using Crocodev.Common.Identifier;

// !>> Core | Sdk | Task
namespace Spreadbot.Sdk.Common
{
    public abstract class AbstractTask : Identifiable<AbstractTask,int>, ITask
    {
        // ===================================================================================== []
        // Tasks
        private readonly IList<ITask> _subTasks = new List<ITask>();
        // --------------------------------------------------------[]
        public AbstractTask AddSubTask(ITask task)
        {
            _subTasks.Add(task);
            return this;
        }

        // ===================================================================================== []
        // Properties
        private static int _totalNum;
        public readonly Identifier Id = (Identifier)(++_totalNum);
        public readonly DateTime CreationTime = DateTime.Now;
        public abstract TaskStatus StatusCode { get; }
        public bool IsCritical = true;

        // ===================================================================================== []
        // ITask
        public virtual string Autoinfo
        {
            get { return "Args:[{0}] Response:[{1}]".SafeFormat(Args, Response == null ? "no" : Response.ToString()); }
        }

        // --------------------------------------------------------[]
        public virtual ITaskArgs Args { get; protected set; }
        // --------------------------------------------------------[]
        public virtual IResponse Response { get; set; }
        // --------------------------------------------------------[]
        public string Description { get; set; }
        // --------------------------------------------------------[]
        public virtual IEnumerable<ITask> SubTasks
        {
            get { return _subTasks; }
        }

        // ===================================================================================== []
        // Object
        public override string ToString()
        {
            return string.IsNullOrEmpty(Description) ? Autoinfo : Description;
        }
    }
}