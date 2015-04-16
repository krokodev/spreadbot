// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Stores
// DemoshopStoreManager.imp.IStore.cs

using System;
using System.Collections.Generic;
using Spreadbot.Core.Abstracts.Channel.Operations.Tasks;
using Spreadbot.Core.Abstracts.Store.Manager;
using Spreadbot.Core.Abstracts.Store.Operations.Tasks;

namespace Spreadbot.Core.Stores.Demoshop.Manager
{
    public partial class DemoshopStoreManager
    {
        public string Id
        {
            get { return "Demoshop"; }
        }

        IEnumerable< IChannelTask > IStoreManager.GetChannelTasks()
        {
            return GetChannelTasks();
        }

        IEnumerable< IStoreTask > IStoreManager.StoreTasks
        {
            get { return StoreTasks; }
        }

        public virtual IStoreTask CreateTask( StoreTaskType taskType )
        {
            switch( taskType ) {
                case StoreTaskType.PublishOnEbay :
                    return CreateTaskPublishOnEbay();
                default :
                    throw new ArgumentException( string.Format( "Unknown taskType: [{0}]", taskType ) );
            }
        }
    }
}