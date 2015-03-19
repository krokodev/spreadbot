// Spreadbot (c) 2015 Crocodev
// Spreadbot.App.Web
// DemoshopStore.state.cs
// romak_000, 2015-03-19 17:31

using System.Collections.Generic;
using Nereal.Serialization;
using Spreadbot.App.Web.Sources.Demoshop.Task;

namespace Spreadbot.App.Web.Sources.Demoshop.Store
{
    // Code: DemoshopStore.state

    public partial class DemoshopStoreManager
    {
        [Serialize]
        private List<DemoshopStoreTask> StoreTasks
        {
            get { return _storeTasks; }
            set { _storeTasks = value; }
        }
    }
}