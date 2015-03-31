﻿// Spreadbot (c) 2015 Crocodev
// Spreadbot.App.Web
// DemoshopModel.cs
// Roman, 2015-03-31 1:25 PM

using System.Collections.Generic;
using System.Linq;
using Spreadbot.Core.Abstracts.Channel.Operations.Tasks;
using Spreadbot.Core.Abstracts.Store.Manager;
using Spreadbot.Core.Abstracts.Store.Operations.Tasks;
using Spreadbot.Core.Stores.Demoshop.Items;
using Spreadbot.Core.Stores.Demoshop.Manager;
using Spreadbot.Core.Stores.Demoshop.Operations.Tasks;
using Spreadbot.Sdk.Common.Operations.Tasks;

namespace Spreadbot.App.Web.Models
{
    public class DemoshopModel
    {
        public static DemoshopItem Item
        {
            get { return StoreManager.Item; }
        }

        public static IEnumerable< IStoreTask > StoreTasks
        {
            get { return ( ( IStoreManager ) StoreManager ).StoreTasks; }
        }

        public static IEnumerable< IChannelTask > ChannelTasksTodo
        {
            get { return ChannelTasks.Where( t => t.GetStatusCode() == TaskStatus.Todo ); }
        }

        public static IEnumerable< IChannelTask > ChannelTasksInprocess
        {
            get { return ChannelTasks.Where( t => t.GetStatusCode() == TaskStatus.Inprocess ); }
        }

        public static IEnumerable< IChannelTask > ChannelTasks
        {
            get { return ( ( IStoreManager ) StoreManager ).GetChannelTasks(); }
        }

        public static DemoshopStoreManager StoreManager
        {
            get { return DemoshopStoreManager.Instance; }
        }

        public static string ErrorMessage
        {
            get { return DemoshopStoreManager.Instance.ErrorMessage; }
        }

        public static void SaveItem( DemoshopItem item )
        {
            StoreManager.SaveItem( item );
        }

        public static void CreateTaskPublishItemOnEbay()
        {
            StoreManager.CreateTask( DemoshopStoreTaskType.PublishOnEbay );
        }

        private static readonly object Locker = 0;

        public static void Save()
        {
            lock( Locker ) {
                StoreManager.SaveData();
            }
        }

        public static void Restore()
        {
            lock( Locker ) {
                StoreManager.RestoreData();
            }
        }

        public static void DeleteTasks()
        {
            lock( Locker ) {
                StoreManager.DeleteAllTasks();
            }
        }

        public static AbstractTask FindTask( string taskId )
        {
            return StoreManager.FindTask( taskId );
        }
    }
}