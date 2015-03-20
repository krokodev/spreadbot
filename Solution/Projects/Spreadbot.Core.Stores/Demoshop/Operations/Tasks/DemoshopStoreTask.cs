// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Stores
// DemoshopStoreTask.cs
// romak_000, 2015-03-20 13:57

using Spreadbot.Core.Abstracts.Store.Operations.Tasks;
using Spreadbot.Sdk.Common.Operations.Tasks;

namespace Spreadbot.Core.Stores.Demoshop.Operations.Tasks
{
    public class DemoshopStoreTask : AbstractStoreTask
    {
        // ===================================================================================== []
        // Ctor
        public DemoshopStoreTask() {}

        // --------------------------------------------------------[]
        public DemoshopStoreTask( string storeId, string description )
            : base( storeId, description ) {}

        // ===================================================================================== []
        // Public
        public override TaskStatus GetStatusCode()
        {
            return CalcSuperTaskStatusCode();
        }
    }
}