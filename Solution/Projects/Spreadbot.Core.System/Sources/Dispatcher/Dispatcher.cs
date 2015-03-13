using System.Collections.Generic;
using MoreLinq;
using Spreadbot.Core.Common;
using Spreadbot.Sdk.Common;

// >> Core | Dispatcher

namespace Spreadbot.Core.System
{
    public class Dispatcher
    {
        // ===================================================================================== []
        // RunChannelTask
        public static void RunChannelTask(IChannelTask task)
        {
            if (task.StatusCode!=TaskStatus.Todo)
            {
                throw new SpreadbotException("Task was already run [{0}]", task);
            }

            switch (task.Method)
            {
                case ChannelMethod.Publish:
                    task.Channel.Publish(task);
                    break;
                default:
                    throw new SpreadbotException("Unexpected task operation [{0}]", task.Method);
            }
        }

        // --------------------------------------------------------[]
        public static void RunChannelTasks(IEnumerable<IChannelTask> tasks)
        {
            tasks.ForEach(RunChannelTask);
        }

        // ===================================================================================== []
        // ProceedChannelTasks
        // Code: Dispatcher : ProceedChannelTasks
        private static void ProceedChannelTask(IChannelTask task)
        {
            if (task.StatusCode != TaskStatus.Inprocess)
            {
                throw new SpreadbotException("Task is not In-Process [{0}]", task);
            }

            task.Channel.ProceedTask(task);
        }

        // --------------------------------------------------------[]
        public static void ProceedChannelTasks(IEnumerable<IChannelTask> tasks)
        {
            tasks.ForEach(ProceedChannelTask);
        }

        // --------------------------------------------------------[]
    }
}