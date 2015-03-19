// Spreadbot (c) 2015 Crocodev
// Spreadbot.Sdk.Common
// AbstractTask.cs
// romak_000, 2015-03-19 15:38

namespace Spreadbot.Sdk.Common.Operations.Tasks
{
    public abstract partial class AbstractTask : ITask
    {
        // ===================================================================================== []
        // Ctor
        protected AbstractTask()
        {
            IsCritical = true;
        }

        // ===================================================================================== []
        // Public
        public override string ToString()
        {
            return Autoinfo;
        }

        // --------------------------------------------------------[]
        public AbstractTask AddSubTask(AbstractTask task)
        {
            return DoAddSubTask(task);
        }

        // ===================================================================================== []
        // Protected
        protected TaskStatus CalcSuperTaskStatusCode()
        {
            return DoCalcSuperTaskStatusCode();
        }
    }
}