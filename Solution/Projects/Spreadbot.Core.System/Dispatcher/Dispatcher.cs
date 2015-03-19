// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.System
// Dispatcher.cs
// romak_000, 2015-03-19 15:49

using System.Collections.Generic;
using MoreLinq;
using Spreadbot.Core.Common.Channel.Operations.Methods;
using Spreadbot.Core.Common.Channel.Operations.Tasks;
using Spreadbot.Sdk.Common.Exceptions;
using Spreadbot.Sdk.Common.Operations.Tasks;

// >> Core | Dispatcher

namespace Spreadbot.Core.System.Dispatcher
{
    public class Dispatcher
    {
        // ===================================================================================== []
        // RunChannelTask
        public static void RunChannelTask(IChannelTask task)
        {
            if (task.GetStatusCode() != TaskStatus.Todo)
            {
                throw new SpreadbotException("Task was already run [{0}]", task);
            }

            switch (task.ChannelMethod)
            {
                case ChannelMethod.Publish:
                    task.ChannelRef.Publish(task);
                    break;
                default:
                    throw new SpreadbotException("Unexpected task operation [{0}]", task.ChannelMethod);
            }
        }

        // --------------------------------------------------------[]
        public static void RunChannelTasks(IEnumerable<IChannelTask> tasks)
        {
            tasks.ForEach(RunChannelTask);
        }

        // ===================================================================================== []
        // ProceedChannelTasks
        private static void ProceedChannelTask(IChannelTask task)
        {
            if (task.GetStatusCode() != TaskStatus.Inprocess)
            {
                throw new SpreadbotException("Task is not In-Process [{0}]", task);
            }

            task.ChannelRef.ProceedTask(task);
        }

        // --------------------------------------------------------[]
        public static void ProceedChannelTasks(IEnumerable<IChannelTask> tasks)
        {
            tasks.ForEach(ProceedChannelTask);
        }

        // --------------------------------------------------------[]
    }
}