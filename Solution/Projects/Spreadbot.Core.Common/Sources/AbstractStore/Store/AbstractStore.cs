using System.Collections.Generic;
using System.Linq;
using MoreLinq;
using Spreadbot.Sdk.Common;

namespace Spreadbot.Core.Common
{
    public abstract class AbstractStore : IStore
    {
        // ===================================================================================== []
        // Tasks
        private List<AbstractTask> _tasks = new List<AbstractTask>();
        // --------------------------------------------------------[]
        public List<AbstractTask> Tasks
        {
            get { return _tasks; }
            set { _tasks = value; }
        }
        // --------------------------------------------------------[]
        protected void AddTask(AbstractTask task, bool withSubTasks = false)
        {
            Tasks.Add(task);
            if (withSubTasks)
            {
                AddTasks(task.SubTasks, true);
            }
        }

        // --------------------------------------------------------[]
        protected void AddTasks(IEnumerable<AbstractTask> tasks, bool withSubTasks = false)
        {
            tasks.ForEach(t => { AddTask(t, withSubTasks); });
        }

        // ===================================================================================== []
        // IStore
        IEnumerable<ITask> IStore.Tasks
        {
            get { return Tasks; }
        }

        // --------------------------------------------------------[]
        IEnumerable<IChannelTask> IStore.ChannelTasks
        {
            get { return Tasks.OfType<IChannelTask>(); }
        }

        // --------------------------------------------------------[]
        IEnumerable<IStoreTask> IStore.StoreTasks
        {
            get { return Tasks.OfType<IStoreTask>(); }
        }

        // --------------------------------------------------------[]
        public abstract string Name { get; }
    }
}