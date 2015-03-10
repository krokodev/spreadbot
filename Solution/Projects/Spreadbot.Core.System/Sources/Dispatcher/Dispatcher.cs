using System.Collections.Generic;
using System.Linq;
using MoreLinq;
using Spreadbot.Core.Common;

// >> Core | Dispatcher

namespace Spreadbot.Core.System
{
    public class Dispatcher
    {
        // Code: Dispatcher : RunChannelTask
        public static void RunChannelTask(IChannelTask task)
        {
            if (task.Response != null)
            {
                throw new SpreadbotException("Task is already done [{0}]", task);
            }

            switch (task.Operation)
            {
                case ChannelOperation.Publish:
                    task.Response = task.Channel.Publish(task.Args);
                    break;
                default:
                    throw new SpreadbotException("Unexpected task operation [{0}]", task.Operation);
            }
        }

        public static void RunChannelTasks(IEnumerable<IChannelTask> tasks)
        {
            tasks.Where(t => t.Response == null).ForEach(RunChannelTask);
        }
    }
}