// Spreadbot (c) 2015 Crocodev
// Spreadbot.App.Web
// DemoshopStore.cs
// romak_000, 2015-03-19 17:20

using System;
using System.Threading;
using Nereal.Serialization;
using Spreadbot.Core.Common.Store;

namespace Spreadbot.App.Web.Sources.Demoshop.Store
{
    public partial class DemoshopStoreManager : IStoreManager
    {
        // ===================================================================================== []
        // Instance
        private static readonly Lazy<DemoshopStoreManager> LazyInstance =
            new Lazy<DemoshopStoreManager>(CreateInstance, LazyThreadSafetyMode.ExecutionAndPublication);

        // --------------------------------------------------------[]
        public DemoshopStoreManager()
        {
            LoadItem();
        }

        // --------------------------------------------------------[]
        private static DemoshopStoreManager CreateInstance()
        {
            return new DemoshopStoreManager();
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
            Serializer.Default.Serialize(this, DataFileName());
        }

        // --------------------------------------------------------[]
        public void Restore()
        {
            Serializer.Default.Deserialize(this, DataFileName());
        }

        // ===================================================================================== []
        // PublishItemOnEbay
        public void PublishItemOnEbay()
        {
            DoPublishOnEbay();
        }
    }
}