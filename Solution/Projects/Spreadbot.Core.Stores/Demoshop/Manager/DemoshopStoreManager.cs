// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Stores
// DemoshopStoreManager.cs
// romak_000, 2015-03-26 19:42

using System;
using System.IO;
using System.Linq;
using System.Threading;
using Nereal.Serialization;
using Spreadbot.Core.Abstracts.Store.Manager;
using Spreadbot.Core.Stores.Demoshop.Operations.Tasks;
using Spreadbot.Sdk.Common.Operations.Tasks;

namespace Spreadbot.Core.Stores.Demoshop.Manager
{
    public partial class DemoshopStoreManager : IStoreManager
    {
        // ===================================================================================== []
        private static readonly Lazy< DemoshopStoreManager > LazyInstance =
            new Lazy< DemoshopStoreManager >(
                () => new DemoshopStoreManager(),
                LazyThreadSafetyMode.ExecutionAndPublication );

        // --------------------------------------------------------[]
        public DemoshopStoreManager()
        {
            LoadItem();
        }

        // --------------------------------------------------------[]
        public static DemoshopStoreManager Instance
        {
            get { return LazyInstance.Value; }
        }

        // ===================================================================================== []
        public void SaveData()
        {
            Serializer.Default.Serialize( this, DataFileName() );
        }

        // --------------------------------------------------------[]
        public void RestoreData()
        {
            try {
                if( File.Exists( DataFileName() ) ) {
                    Serializer.Default.Deserialize( this, DataFileName() );
                }
            }
            catch( Exception ) {
                // ignored
            }
        }

        // ===================================================================================== []
        public
            DemoshopStoreTask CreateTask( DemoshopStoreTaskType taskType )
        {
            switch( taskType ) {
                case DemoshopStoreTaskType.PublishOnEbay :
                    return DoCreateTaskPublishOnEbay();
            }
            throw new ArgumentException( string.Format( "Unknown taskType: [{0}]", taskType ) );
        }

        // --------------------------------------------------------[]
        public void DeleteTask( DemoshopStoreTask task )
        {
            StoreTasks.Remove( task );
        }

        // --------------------------------------------------------[]
        public void DeleteAllTasks()
        {
            StoreTasks.Clear();
        }

        // --------------------------------------------------------[]
        public AbstractTask FindTask( string taskId )
        {
            return ( AbstractTask ) StoreTasks
                .SelectMany( t => t.AbstractSubTasks )
                .Concat( StoreTasks )
                .First( tt => tt.Id == taskId );
        }
    }
}