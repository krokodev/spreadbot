// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Stores
// DemoshopStoreManager.cs
// Roman, 2015-03-31 1:26 PM

using System;
using System.IO;
using System.Linq;
using System.Threading;
using Crocodev.Common.Extensions;
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
            var fileName = DataFileName();
            var tmpFileName = fileName + ".bak";
            try {
                Serializer.Default.Serialize( this, tmpFileName );
                File.Delete( fileName );
                File.Copy( tmpFileName, fileName );
                ErrorMessage = "";
            }
            catch {
                ErrorMessage = "Can't save tasks to [{0}]".SafeFormat( fileName );
            }
        }

        // --------------------------------------------------------[]
        [NotSerialize]
        public string ErrorMessage { get; set; }

        // --------------------------------------------------------[]
        public void RestoreData()
        {
            var fileName = DataFileName();
            try {
                Serializer.Default.Deserialize( this, fileName );
                ErrorMessage = "";
            }
            catch( Exception ) {
                ErrorMessage = "Can't load data from [{0}]".SafeFormat( fileName );
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
    }
}