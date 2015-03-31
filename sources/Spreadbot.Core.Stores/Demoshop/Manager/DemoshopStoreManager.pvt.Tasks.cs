﻿// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Stores
// DemoshopStoreManager.pvt.Tasks.cs
// Roman, 2015-03-31 1:26 PM

using System.Collections.Generic;
using System.Linq;
using Spreadbot.Core.Abstracts.Channel.Operations.Tasks;
using Spreadbot.Core.Stores.Demoshop.Operations.Tasks;

namespace Spreadbot.Core.Stores.Demoshop.Manager
{
    public partial class DemoshopStoreManager
    {
        // --------------------------------------------------------[]
        private List< DemoshopStoreTask > _storeTasks = new List< DemoshopStoreTask >();

        // --------------------------------------------------------[]
        private void AddTask( DemoshopStoreTask task )
        {
            _storeTasks.Add( task );
        }

        // --------------------------------------------------------[]
        public IEnumerable< AbstractChannelTask > GetChannelTasks()
        {
            return StoreTasks.SelectMany( t => t.AbstractSubTasks.Select( cnt => ( AbstractChannelTask ) cnt ) );
        }
    }
}