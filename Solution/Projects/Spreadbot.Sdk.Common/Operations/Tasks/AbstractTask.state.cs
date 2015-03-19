// Spreadbot (c) 2015 Crocodev
// Spreadbot.Sdk.Common
// AbstractTask.state.cs
// romak_000, 2015-03-19 15:38

using System.Collections.Generic;
using Spreadbot.Sdk.Common.Operations.Args;

namespace Spreadbot.Sdk.Common.Operations.Tasks
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