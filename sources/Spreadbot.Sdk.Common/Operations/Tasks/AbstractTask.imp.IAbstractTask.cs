// Spreadbot (c) 2015 Crocodev
// Spreadbot.Sdk.Common
// AbstractTask.imp.IAbstractTask.cs
// Roman, 2015-04-03 8:17 PM

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