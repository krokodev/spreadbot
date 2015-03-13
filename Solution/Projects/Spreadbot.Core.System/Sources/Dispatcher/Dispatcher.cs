using System.Collections.Generic;
using System.Linq;
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
                    task.Response = task.Channel.Publish(task.ChannelArgs);
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
        // UpdateChannelTasks
        // Code: ** Dispatcher : UpdateChannelTasks
        private static void UpdateChannelTask(IChannelTask task)
        {
            if (task.StatusCode != TaskStatus.Inprocess)
            {
                throw new SpreadbotException("Task is not In-Process [{0}]", task);
            }

            task.Channel.Update(task);
        }

        // --------------------------------------------------------[]
        public static void UpdateChannelTasks(IEnumerable<IChannelTask> tasks)
        {
            tasks.ForEach(UpdateChannelTask);
        }

        // --------------------------------------------------------[]
    }
}