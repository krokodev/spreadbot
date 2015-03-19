// Spreadbot (c) 2015 Crocodev
// Spreadbot.App.Web
// DemoshopStore.imp.IStore.cs
// romak_000, 2015-03-19 17:09

using System.Collections.Generic;
using Spreadbot.Core.Common.Channel.Operations.Tasks;
using Spreadbot.Core.Common.Store;
using Spreadbot.Core.Common.Store.Operations;

namespace Spreadbot.App.Web.Sources.Demoshop.Store
{
    // !>> App | Web | DemoshopStore
    public partial class DemoshopStoreManager
    {
        // --------------------------------------------------------[]
        public string Id
        {
            get { return "Demoshop"; }
        }

        // --------------------------------------------------------[]
        IEnumerable<IChannelTask> IStoreManager.GetChannelTasks()
        {
            return GetChannelTasks();
        }

        // --------------------------------------------------------[]
        IEnumerable<IStoreTask> IStoreManager.StoreTasks
        {
            get { return StoreTasks; }
        }
    }
}