// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Stores
// DemoshopStoreManager.cs
// Roman, 2015-04-04 11:23 AM

using System;
using System.Collections.Generic;
using System.Linq;
using Spreadbot.Core.Abstracts.Store.Manager;
using Spreadbot.Core.Channels.Ebay.Operations.Tasks;
using Spreadbot.Core.Stores.Demoshop.Operations.Tasks;
using Spreadbot.Sdk.Common.Operations.Tasks;

namespace Spreadbot.Core.Stores.Demoshop.Manager
{
    public partial class DemoshopStoreManager : IStoreManager, IDisposable
    {
        // --------------------------------------------------------[]
        public DemoshopStoreManager()
        {
            LoadData();
            if( Item == null ) {
                SetItemToDefault();
            }
        }

        // --------------------------------------------------------[]
        public void Dispose()
        {
            SaveData();
        }

        // --------------------------------------------------------[]
        private static readonly object _locker = new object();

        public void SaveData()
        {
            lock( _locker ) {
                _SaveData();
            }
        }

        // --------------------------------------------------------[]
        public void LoadData()
        {
            lock( _locker ) {
                _LoadData();
            }
        }

        // --------------------------------------------------------[]
        public DemoshopStoreTask CreateTask( DemoshopStoreTaskType taskType )
        {
            switch( taskType ) {
                case DemoshopStoreTaskType.PublishOnEbay :
                    return CreateTaskPublishOnEbay();
                default :
                    throw new ArgumentException( string.Format( "Unknown taskType: [{0}]", taskType ) );
            }
        }

        // --------------------------------------------------------[]
        public void DeleteAllTasks()
        {
            StoreTasks.Clear();
        }

        // --------------------------------------------------------[]
        public AbstractTask FindTask( string taskId )
        {
            try {
                return ( AbstractTask ) StoreTasks
                    .SelectMany( t => t.AbstractSubTasks )
                    .Concat( StoreTasks )
                    .First( tt => tt.Id == taskId );
            }
            catch {
                return null;
            }
        }

        // --------------------------------------------------------[]
        public IEnumerable< EbayPublishTask > GetEbayPublishTasks()
        {
            return GetChannelTasks().OfType< EbayPublishTask >();
        }
    }
}