// Spreadbot (c) 2015 Crocodev
// Spreadbot.App.Web
// DemoshopStore.pvt.Tasks.cs
// romak_000, 2015-03-19 17:12

using System.Collections.Generic;
using System.Linq;
using Spreadbot.App.Web.Sources.Demoshop.Task;
using Spreadbot.Core.Common.Channel.Operations.Tasks;

namespace Spreadbot.App.Web.Sources.Demoshop.Store
{
    public partial class DemoshopStoreManager
    {
        // --------------------------------------------------------[]
        private List<DemoshopStoreTask> _storeTasks = new List<DemoshopStoreTask>();
        // --------------------------------------------------------[]
        private void AddTask(DemoshopStoreTask task)
        {
            _storeTasks.Add(task);
        }

        // --------------------------------------------------------[]
        public IEnumerable<AbstractChannelTask> GetChannelTasks()
        {
            return StoreTasks.SelectMany(t => t.SubTasks.Select(cnt => (AbstractChannelTask) cnt));
        }
    }
}