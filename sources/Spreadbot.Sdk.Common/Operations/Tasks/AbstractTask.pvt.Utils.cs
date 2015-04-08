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
        // Code: _CalcSuperTaskStatusCode, wake-up bug
        private TaskStatus _CalcSuperTaskStatusCode()
        {
            if( AbstractSubTasks == null ) {
                throw  new SpreadbotException( "Task [{0}] AbstractSubTasks == null", Id );
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

            throw new SpreadbotException( "Can't calculate Status StatusCode" );
        }
    }
}