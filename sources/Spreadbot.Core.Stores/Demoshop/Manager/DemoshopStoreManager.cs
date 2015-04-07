// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Stores
// DemoshopStoreManager.cs
// Roman, 2015-04-07 2:43 PM

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
        private static readonly object Locker = new object();

        public void SaveData()
        {
            lock( Locker ) {
                _SaveData();
            }
        }

        // --------------------------------------------------------[]
        public void LoadData()
        {
            lock( Locker ) {
                _LoadData();
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

        // --------------------------------------------------------[]

        public void AddTask( DemoshopStoreTask storeTask )
        {
            _AddTask( storeTask );
        }
    }
}