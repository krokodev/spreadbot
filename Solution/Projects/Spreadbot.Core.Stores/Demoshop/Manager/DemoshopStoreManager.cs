// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Stores
// DemoshopStoreManager.cs
// romak_000, 2015-03-20 16:35

using System;
using System.Threading;
using Crocodev.Common.Extensions;
using Nereal.Serialization;
using Spreadbot.Core.Abstracts.Store.Manager;
using Spreadbot.Core.Stores.Demoshop.Operations.Tasks;

namespace Spreadbot.Core.Stores.Demoshop.Manager
{
    public partial class DemoshopStoreManager : IStoreManager
    {
        // ===================================================================================== []
        // Instance
        private static readonly Lazy< DemoshopStoreManager > LazyInstance =
            new Lazy< DemoshopStoreManager >(
                () => new DemoshopStoreManager(),
                LazyThreadSafetyMode.ExecutionAndPublication );

        // --------------------------------------------------------[]
        private DemoshopStoreManager()
        {
            LoadItem();
        }

        // --------------------------------------------------------[]
        public static DemoshopStoreManager Instance
        {
            get { return LazyInstance.Value; }
        }

        // ===================================================================================== []
        // Save/Restore
        public void Save()
        {
            Serializer.Default.Serialize( this, DataFileName() );
        }

        // --------------------------------------------------------[]
        public void Restore()
        {
            Serializer.Default.Deserialize( this, DataFileName() );
        }

        // ===================================================================================== []
        // PublishItemOnEbay
        public DemoshopStoreTask CreateTask( DemoshopStoreTaskType taskType )
        {
            switch( taskType ) {
                case DemoshopStoreTaskType.PublishOnEbay :
                    return DoCreateTaskPublishOnEbay();
            }
            throw new ArgumentException( "Unknown taskType: [{0}]".SafeFormat( taskType ) );
        }
    }
}