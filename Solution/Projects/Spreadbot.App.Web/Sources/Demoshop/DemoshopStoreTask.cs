using Spreadbot.Core.Common;
using Spreadbot.Sdk.Common;

namespace Spreadbot.App.Web
{
    public class DemoshopStoreTask : AbstractStoreTask
    {
        public DemoshopStoreTask(string description)
            : base(description)
        {
        }

        public override TaskStatus StatusCode
        {
            get { return CalcSuperTaskStatusCode(); }
        }
    }
}