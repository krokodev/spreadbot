// Spreadbot (c) 2015 Crocodev
// Spreadbot.Sdk.Common
// AbstractTask.cs

using System;
using Crocodev.Common.Extensions;
using NLog;
using Spreadbot.Sdk.Common.Crocodev.Common;

namespace Spreadbot.Sdk.Common.Operations.Tasks
{
    public abstract partial class AbstractTask : IAbstractTask
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

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
            catch( Exception e ) {
                Logger.ErrorException( "Task [{0}] status can't be calculated".SafeFormat( Id ), e );
                return TaskStatus.Unknown;
            }
        }

        public abstract string GetBriefInfo();
    }
}