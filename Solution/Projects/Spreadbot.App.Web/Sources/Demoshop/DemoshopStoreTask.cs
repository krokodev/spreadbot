using Spreadbot.Core.Common;
using Spreadbot.Sdk.Common;

namespace Spreadbot.App.Web
{
    public class DemoshopStoreTask : AbstractStoreTask
    {
        public DemoshopStoreTask()
        {
            
        }
        public DemoshopStoreTask(IStore storeRef, string description)
            : base(storeRef, description)
        {
        }

        public override TaskStatus StatusCode
        {
            get { return CalcSuperTaskStatusCode(); }
        }
    }
}