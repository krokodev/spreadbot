// Spreadbot (c) 2015 Crocodev
// Spreadbot.Sdk.Common
// AbstractTask.cs
// Roman, 2015-04-07 2:58 PM

using System;
using Spreadbot.Sdk.Common.Crocodev.Common;
using Spreadbot.Sdk.Common.Exceptions;

namespace Spreadbot.Sdk.Common.Operations.Tasks
{
    public abstract partial class AbstractTask : IAbstractTask
    {
        protected AbstractTask()
        {
            CreationTime = DateTime.Now;
            LastUpdateTime = DateTime.Now;
            Id = Guid.NewGuid().ToString();
        }

        public override string ToString()
        {
            return this.ToYamlString();
        }

        protected TaskStatus CalcSuperTaskStatusCode()
        {
            try {
                return _CalcSuperTaskStatusCode();
            }
            catch( Exception e) {
                // Todo: Log exception
                throw;
                return TaskStatus.Unknown;
            }
        }

        public abstract string GetBriefInfo();
    }
}