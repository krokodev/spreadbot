// !>> Core | Sdk | AbstractTask.state

using System.Collections.Generic;

namespace Spreadbot.Sdk.Common
{
    public abstract partial class AbstractTask
    {
        // ===================================================================================== []
        // Public
        // --------------------------------------------------------[]
        public bool IsCritical { get; set; }
        // --------------------------------------------------------[]
        public List<AbstractTask> SubTasks
        {
            get { return _subTasks; }
            set { _subTasks = value; }
        }

        // ===================================================================================== []
        // Protected
        // Todo: Ref: Is AbstractTask.AbstractArg neccessary?
        protected AbstractArgs AbstractArgs { get; set; }
    }
}