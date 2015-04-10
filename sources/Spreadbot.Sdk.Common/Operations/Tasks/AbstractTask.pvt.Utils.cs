// Spreadbot (c) 2015 Crocodev
// Spreadbot.Sdk.Common
// AbstractTask.pvt.Utils.cs

using System.Linq;
using Spreadbot.Sdk.Common.Exceptions;

namespace Spreadbot.Sdk.Common.Operations.Tasks
{
    public abstract partial class AbstractTask
    {
        private TaskStatus _CalcSuperTaskStatusCode()
        {
            /**/
            Logger.Trace( "_CalcSuperTaskStatusCode():" );
            Logger.Trace( "1" );

            if( AbstractSubTasks == null ) {
                return TaskStatus.Unknown;
            }

            /**/
            Logger.Trace( "2" );

            var totalSubCount = AbstractSubTasks.Count();

            /**/
            Logger.Trace( "3" );

            if( totalSubCount == 0 ) {
                return TaskStatus.Unknown;
            }

            /**/
            Logger.Trace( "4" );

            if( AbstractSubTasks.Any( t => t.GetStatusCode() == TaskStatus.Unknown ) ) {
                return TaskStatus.Unknown;
            }

            /**/
            Logger.Trace( "5" );

            if( AbstractSubTasks.Count( t => t.GetStatusCode() == TaskStatus.Todo ) == totalSubCount ) {
                return TaskStatus.Todo;
            }

            /**/
            Logger.Trace( "6" );

            if( AbstractSubTasks.Count( t => t.GetStatusCode() == TaskStatus.Success ) == totalSubCount ) {
                return TaskStatus.Success;
            }

            /**/
            Logger.Trace( "7" );

            if( AbstractSubTasks.Any( t => t.IsCritical && t.GetStatusCode() == TaskStatus.Failure ) ||
                AbstractSubTasks.Count( t => t.GetStatusCode() == TaskStatus.Failure ) == totalSubCount ) {
                return TaskStatus.Failure;
            }

            /**/
            Logger.Trace( "8" );

            if( AbstractSubTasks.Count( t => t.GetStatusCode() == TaskStatus.Todo ) +
                AbstractSubTasks.Count( t => t.GetStatusCode() == TaskStatus.Inprocess ) +
                AbstractSubTasks.Count( t => t.GetStatusCode() == TaskStatus.Success ) +
                AbstractSubTasks.Count( t => t.GetStatusCode() == TaskStatus.Failure && !t.IsCritical )
                == totalSubCount ) {
                return TaskStatus.Inprocess;
            }

            /**/
            Logger.Trace( "9" );

            throw new SpreadbotException( "Can't calculate StatusCode" );
        }
    }
}