using System.Collections.Generic;
using System.Linq;
using MoreLinq;
using Spreadbot.Core.Common;

namespace Spreadbot.Core.System
{
    public class Store : IStore
    {
        // ===================================================================================== []
        // Tasks
        private readonly IList<ITask> _tasks = new List<ITask>();
        // --------------------------------------------------------[]
        protected void AddTask(ITask task, bool withSubTasks = false)
        {
            _tasks.Add(task);
            if (withSubTasks)
                AddTasks(task.SubTasks, true);
        }

        // --------------------------------------------------------[]
        protected void AddTasks(IEnumerable<ITask> tasks, bool withSubTasks = false)
        {
            tasks.ForEach(t => { AddTask(t, withSubTasks); });
        }

        // ===================================================================================== []
        // IStore
        public IEnumerable<ITask> Tasks
        {
            get { return _tasks; }
        }

        // --------------------------------------------------------[]
        public IEnumerable<IChannelTask> ChannelTasks
        {
            get { return Tasks.OfType<IChannelTask>(); }
        }

        // --------------------------------------------------------[]
        public IEnumerable<IStoreTask> StoreTasks
        {
            get { return Tasks.OfType<IStoreTask>(); }
        }
    }
}