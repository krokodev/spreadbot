// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.System
// Dispatcher.pvt.ChannelTasks.cs
// romak_000, 2015-03-20 13:56

using System.Collections.Generic;
using MoreLinq;
using Spreadbot.Core.Abstracts.Chanel.Operations.Methods;
using Spreadbot.Core.Abstracts.Chanel.Operations.Tasks;
using Spreadbot.Sdk.Common.Exceptions;
using Spreadbot.Sdk.Common.Operations.Tasks;

namespace Spreadbot.Core.System.Dispatcher
{
    public partial class Dispatcher
    {
        // ===================================================================================== []
        // DoRunChannelTask
        private void DoRunChannelTask( IChannelTask task )
        {
            if( task.GetStatusCode() != TaskStatus.Todo ) {
                throw new SpreadbotException( "Task was already run [{0}]", task );
            }

            switch( task.ChannelMethod ) {
                case ChannelMethod.Publish :
                    FindChannel( task.ChannelId ).RunPublishTask( task );
                    break;
                default :
                    throw new SpreadbotException( "Unexpected task operation [{0}]", task.ChannelMethod );
            }
        }

        // ===================================================================================== []
        // DoProceedChannelTasks
        private void DoProceedChannelTasks( IEnumerable< IChannelTask > tasks )
        {
            tasks.ForEach( DoProceedChannelTask );
        }

        // --------------------------------------------------------[]
        private void DoProceedChannelTask( IChannelTask task )
        {
            if( task.GetStatusCode() != TaskStatus.Inprocess ) {
                throw new SpreadbotException( "Task is not In-Process [{0}]", task );
            }

            FindChannel( task.ChannelId ).ProceedTask( task );
        }
    }
}