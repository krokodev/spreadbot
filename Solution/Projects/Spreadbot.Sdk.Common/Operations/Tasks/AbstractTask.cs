// Spreadbot (c) 2015 Crocodev
// Spreadbot.Sdk.Common
// AbstractTask.cs
// romak_000, 2015-03-20 19:13

namespace Spreadbot.Sdk.Common.Operations.Tasks
{
    public abstract partial class AbstractTask : IAbstractTask
    {
        protected AbstractTask()
        {
            IsCritical = true;
        }

        public override string ToString()
        {
            return GetAutoinfo();
        }

        protected TaskStatus CalcSuperTaskStatusCode()
        {
            return DoCalcSuperTaskStatusCode();
        }
    }
}