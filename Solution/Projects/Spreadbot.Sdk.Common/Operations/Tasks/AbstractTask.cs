// Spreadbot (c) 2015 Crocodev
// Spreadbot.Sdk.Common
// AbstractTask.cs
// romak_000, 2015-03-20 21:22

namespace Spreadbot.Sdk.Common.Operations.Tasks
{
    public abstract partial class AbstractTask : IAbstractTask
    {
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