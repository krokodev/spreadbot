// Spreadbot (c) 2015 Crocodev
// Spreadbot.App.Web
// DemoshopStoreTask.cs
// romak_000, 2015-03-19 14:07

using Spreadbot.Core.Common.Store;
using Spreadbot.Core.Common.StoreOperations;
using Spreadbot.Sdk.Common.Operations.Tasks;

namespace Spreadbot.App.Web.Sources.Demoshop
{
    public class DemoshopStoreTask : AbstractStoreTask
    {
        // ===================================================================================== []
        // Ctor
        public DemoshopStoreTask()
        {
        }

        // --------------------------------------------------------[]
        public DemoshopStoreTask(IStore storeRef, string description)
            : base(storeRef, description)
        {
        }

        // ===================================================================================== []
        // Public
        public override TaskStatus GetStatusCode()
        {
            return CalcSuperTaskStatusCode();
        }
    }
}