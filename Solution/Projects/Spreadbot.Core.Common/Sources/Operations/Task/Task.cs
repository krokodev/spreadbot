using System;
using System.Collections.Generic;
using Crocodev.Common;
using Crocodev.Common.Identifier;

namespace Spreadbot.Core.Common
{
    public abstract class Task : Identifiable<Task,int>, ITask
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
        // Properties
        private static int _totalNum;
        public readonly Identifier Id = (Identifier)(++_totalNum);
        public readonly DateTime CreationTime = DateTime.Now;
        public abstract TaskStatusCode StatusCode { get; }
        public bool IsCritical = false;


        // ===================================================================================== []
        // ITask
        public virtual string Autoinfo
        {
            get { return "Args:[{0}] Response:[{1}]".SafeFormat(Args, Response == null ? "no" : Response.ToString()); }
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