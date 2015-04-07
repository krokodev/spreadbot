// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Stores
// DemoshopStoreManager.imp.IStore.cs
// Roman, 2015-04-07 12:23 PM

using System.Collections.Generic;
using Spreadbot.Core.Abstracts.Channel.Operations.Tasks;
using Spreadbot.Core.Abstracts.Store.Manager;
using Spreadbot.Core.Abstracts.Store.Operations.Tasks;

namespace Spreadbot.Core.Stores.Demoshop.Manager
{
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