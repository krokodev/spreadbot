// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Stores
// DemoshopStoreManager.imp.IStore.cs
// romak_000, 2015-03-21 2:11

using System.Collections.Generic;
using Spreadbot.Core.Abstracts.Chanel.Operations.Tasks;
using Spreadbot.Core.Abstracts.Store.Manager;
using Spreadbot.Core.Abstracts.Store.Operations.Tasks;

namespace Spreadbot.Core.Stores.Demoshop.Manager
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
        IEnumerable< IChannelTask > IStoreManager.GetChannelTasks()
        {
            return GetChannelTasks();
        }

        // --------------------------------------------------------[]
        IEnumerable< IStoreTask > IStoreManager.StoreTasks
        {
            get { return StoreTasks; }
        }
    }
}