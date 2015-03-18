// !>> Core | Sdk | AbstractTask

namespace Spreadbot.Sdk.Common
{
    public abstract partial class AbstractTask : IHierarchicalTask
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