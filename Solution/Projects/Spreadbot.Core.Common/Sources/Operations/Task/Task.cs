using System.Collections.Generic;
using Crocodev.Common;

namespace Spreadbot.Core.Common
{
    public abstract class Task : ITask
    {
        // ===================================================================================== []
        // Tasks
        private readonly IList<ITask> _subTasks = new List<ITask>();

        // --------------------------------------------------------[]
        public Task AddSubTask(ITask task)
        {
            _subTasks.Add(task);
            return this;
        }

        // ===================================================================================== []
        // ITask
        public virtual string Autoinfo
        {
            get { return "Args:[{0}] Response:[{1}]".SafeFormat(Args, Response == null ? "no" : Response.Autoinfo); }
        }

        // --------------------------------------------------------[]
        public virtual IArgs Args { get; protected set; }
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