// Spreadbot (c) 2015 Crocodev
// Spreadbot.App.Web
// DemoshopModel.cs
// Roman, 2015-04-07 2:56 PM

using System;
using System.Collections.Generic;
using System.Linq;
using Spreadbot.Core.Abstracts.Channel.Operations.Tasks;
using Spreadbot.Core.Abstracts.Store.Manager;
using Spreadbot.Core.Abstracts.Store.Operations.Tasks;
using Spreadbot.Core.Stores.Demoshop.Items;
using Spreadbot.Core.Stores.Demoshop.Manager;
using Spreadbot.Sdk.Common.Operations.Tasks;

namespace Spreadbot.App.Web.Models
{
    public class DemoshopModel : IDisposable
    {
        // --------------------------------------------------------[]
        private DemoshopStoreManager StoreManager { get; set; }

        // --------------------------------------------------------[]
        public DemoshopModel()
        {
            StoreManager = new DemoshopStoreManager();
        }

        // --------------------------------------------------------[]
        public DemoshopItem Item
        {
            get { return StoreManager.Item; }
        }

        // --------------------------------------------------------[]
        public IEnumerable< IStoreTask > StoreTasks
        {
            get { return ( ( IStoreManager ) StoreManager ).StoreTasks; }
        }

        // --------------------------------------------------------[]
        public IEnumerable< IChannelTask > ChannelTasksTodo
        {
            get { return ChannelTasks.Where( t => t.GetStatusCode() == TaskStatus.Todo ); }
        }

        // --------------------------------------------------------[]
        public IEnumerable< IChannelTask > ChannelTasksInprocess
        {
            get { return ChannelTasks.Where( t => t.GetStatusCode() == TaskStatus.Inprocess ); }
        }

        // --------------------------------------------------------[]
        public IEnumerable< IChannelTask > ChannelTasks
        {
            get { return ( ( IStoreManager ) StoreManager ).GetChannelTasks(); }
        }

        // --------------------------------------------------------[]
        public string Message
        {
            get { return StoreManager.Message; }
            set { StoreManager.Message = value; }
        }

        // --------------------------------------------------------[]
        public void UpdateItem( DemoshopItem item )
        {
            StoreManager.UpdateItem( item );
        }

        // --------------------------------------------------------[]
        public void SetItemToDefault()
        {
            StoreManager.SetItemToDefault();
        }

        // --------------------------------------------------------[]
        public void CreateTaskPublishItemOnEbay()
        {
            StoreManager.CreateTask( StoreTaskType.PublishOnEbay );
        }

        // --------------------------------------------------------[]
        public void DeleteTasks()
        {
            StoreManager.DeleteAllTasks();
        }

        // --------------------------------------------------------[]
        public AbstractTask FindTask( string taskId )
        {
            return StoreManager.FindTask( taskId );
        }

        // --------------------------------------------------------[]
        public void Dispose()
        {
            StoreManager.Dispose();
        }
    }
}