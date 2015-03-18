using Spreadbot.Core.Common;
using Spreadbot.Sdk.Common;

namespace Spreadbot.App.Web
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