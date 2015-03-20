// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Stores
// DemoshopStoreManager.state.cs
// romak_000, 2015-03-20 16:18

using System.Collections.Generic;
using Nereal.Serialization;
using Spreadbot.Core.Stores.Demoshop.Operations.Tasks;

namespace Spreadbot.Core.Stores.Demoshop.Manager
{
    // Code: DemoshopStore.state

    public partial class DemoshopStoreManager
    {
        [Serialize]
        public List< DemoshopStoreTask > StoreTasks
        {
            get { return _storeTasks; }
            set { _storeTasks = value; }
        }
    }
}