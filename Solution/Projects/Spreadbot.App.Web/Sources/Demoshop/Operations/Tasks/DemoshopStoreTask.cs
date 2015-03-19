// Spreadbot (c) 2015 Crocodev
// Spreadbot.App.Web
// DemoshopStoreTask.cs
// romak_000, 2015-03-19 16:47

using Spreadbot.Core.Common.Store.Operations;
using Spreadbot.Sdk.Common.Operations.Tasks;

namespace Spreadbot.App.Web.Sources.Demoshop.Task
{
    public class DemoshopStoreTask : AbstractStoreTask
    {
        // ===================================================================================== []
        // Ctor
        public DemoshopStoreTask()
        {
        }

        // --------------------------------------------------------[]
        public DemoshopStoreTask(string storeId, string description)
            : base(storeId, description)
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