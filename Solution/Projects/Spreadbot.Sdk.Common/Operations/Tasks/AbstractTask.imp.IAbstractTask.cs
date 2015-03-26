// Spreadbot (c) 2015 Crocodev
// Spreadbot.Sdk.Common
// AbstractTask.imp.IAbstractTask.cs
// romak_000, 2015-03-26 19:42

using System.Collections.Generic;
using Spreadbot.Sdk.Common.Operations.Responses;

namespace Spreadbot.Sdk.Common.Operations.Tasks
{
    public abstract partial class AbstractTask
    {
        private readonly IEnumerable< IAbstractTask > _abstractSubTasks = new List< IAbstractTask >();

        public abstract TaskStatus GetStatusCode();

        public abstract IAbstractResponse AbstractResponse { get; set; }

        public virtual IEnumerable< IAbstractTask > AbstractSubTasks
        {
            get { return _abstractSubTasks; }
        }
    }
}