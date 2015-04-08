// Spreadbot (c) 2015 Crocodev
// Spreadbot.Sdk.Common
// AbstractTask.pvt.Utils.cs
// Roman, 2015-04-07 3:31 PM

using System.Diagnostics;
using System.Linq;
using Spreadbot.Sdk.Common.Exceptions;

namespace Spreadbot.Sdk.Common.Operations.Tasks
{
    public abstract partial class AbstractTask
    {
        // --------------------------------------------------------[]
        // Todo: Remove Tracing
        private TaskStatus _CalcSuperTaskStatusCode()
        {
            Trace.TraceInformation( "_CalcSuperTaskStatusCode({0})", Id );

            if( AbstractSubTasks == null ) {
                throw  new SpreadbotException( "Task [{0}] AbstractSubTasks == null", Id );
            }

            var totalSubCount = AbstractSubTasks.Count();

            Trace.TraceInformation( "_CalcSuperTaskStatusCode() 1" );

            if( AbstractSubTasks.Any( t => t.GetStatusCode() == TaskStatus.Unknown ) ) {
                return TaskStatus.Unknown;
            }

            Trace.TraceInformation( "_CalcSuperTaskStatusCode() 2" );

            if( AbstractSubTasks.Count( t => t.GetStatusCode() == TaskStatus.Todo ) == totalSubCount ) {
                return TaskStatus.Todo;
            }

            Trace.TraceInformation( "_CalcSuperTaskStatusCode() 3" );

            if( AbstractSubTasks.Count( t => t.GetStatusCode() == TaskStatus.Success ) == totalSubCount ) {
                return TaskStatus.Success;
            }
            Trace.TraceInformation( "_CalcSuperTaskStatusCode() 4" );

            if( AbstractSubTasks.Any( t => t.IsCritical && t.GetStatusCode() == TaskStatus.Failure ) ||
                AbstractSubTasks.Count( t => t.GetStatusCode() == TaskStatus.Failure ) == totalSubCount ) {
                return TaskStatus.Failure;
            }
            Trace.TraceInformation( "_CalcSuperTaskStatusCode() 5" );

            if( AbstractSubTasks.Count( t => t.GetStatusCode() == TaskStatus.Todo ) +
                AbstractSubTasks.Count( t => t.GetStatusCode() == TaskStatus.Inprocess ) +
                AbstractSubTasks.Count( t => t.GetStatusCode() == TaskStatus.Success ) +
                AbstractSubTasks.Count( t => t.GetStatusCode() == TaskStatus.Failure && !t.IsCritical )
                == totalSubCount ) {
                return TaskStatus.Inprocess;
            }

            throw new SpreadbotException( "Can't calculate Status StatusCode" );
        }
    }
}