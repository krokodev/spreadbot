// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Stores
// DemoshopStoreManager.state.cs
// Roman, 2015-04-10 1:28 PM

using System.Collections.Generic;
using Nereal.Serialization;
using Spreadbot.Core.Stores.Demoshop.Items;
using Spreadbot.Core.Stores.Demoshop.Operations.Tasks;

namespace Spreadbot.Core.Stores.Demoshop.Manager
{
    public partial class DemoshopStoreManager
    {
        [Serialize]
        public List< DemoshopStoreTask > StoreTasks
        {
            get { return _storeTasks; }
            set { _storeTasks = value; }
        }

        [Serialize]
        public string Message { get; set; }

        [Serialize]
        public DemoshopItem Item { get; set; }
    }
}