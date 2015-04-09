// Spreadbot (c) 2015 Crocodev
// Spreadbot.Sdk.Common
// AbstractTask.pvt.Utils.cs
// Roman, 2015-04-09 2:13 PM

using System.Linq;
using Spreadbot.Sdk.Common.Exceptions;

namespace Spreadbot.Sdk.Common.Operations.Tasks
{
    public abstract partial class AbstractTask
    {
        private TaskStatus _CalcSuperTaskStatusCode()
        {
            // Code: Todo: Use Nlog to trace _CalcSuperTaskStatusCode
            // Todo: Create Tester class to rise exception
            if( AbstractSubTasks == null ) {
                return TaskStatus.Unknown;
            }

            var totalSubCount = AbstractSubTasks.Count();

            if( totalSubCount == 0 ) {
                return TaskStatus.Unknown;
            }

            if( AbstractSubTasks.Any( t => t.GetStatusCode() == TaskStatus.Unknown ) ) {
                return TaskStatus.Unknown;
            }

            if( AbstractSubTasks.Count( t => t.GetStatusCode() == TaskStatus.Todo ) == totalSubCount ) {
                return TaskStatus.Todo;
            }

            if( AbstractSubTasks.Count( t => t.GetStatusCode() == TaskStatus.Success ) == totalSubCount ) {
                return TaskStatus.Success;
            }

            if( AbstractSubTasks.Any( t => t.IsCritical && t.GetStatusCode() == TaskStatus.Failure ) ||
                AbstractSubTasks.Count( t => t.GetStatusCode() == TaskStatus.Failure ) == totalSubCount ) {
                return TaskStatus.Failure;
            }

            if( AbstractSubTasks.Count( t => t.GetStatusCode() == TaskStatus.Todo ) +
                AbstractSubTasks.Count( t => t.GetStatusCode() == TaskStatus.Inprocess ) +
                AbstractSubTasks.Count( t => t.GetStatusCode() == TaskStatus.Success ) +
                AbstractSubTasks.Count( t => t.GetStatusCode() == TaskStatus.Failure && !t.IsCritical )
                == totalSubCount ) {
                return TaskStatus.Inprocess;
            }

            throw new SpreadbotException( "Can't calculate StatusCode" );
        }
    }
}