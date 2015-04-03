// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Stores
// DemoshopStoreManager.state.cs
// Roman, 2015-04-03 1:44 PM

using System.Collections.Generic;
using Nereal.Serialization;
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
    }
}