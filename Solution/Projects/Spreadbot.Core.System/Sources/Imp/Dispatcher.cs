using System.Collections.Generic;
using System.Linq;

namespace Spreadbot.Core.System
{
    // >> | Core | Dispatcher *
    public class Dispatcher
    {
        public static void Run(IChannelTask task)
        {
            task.Response =  task.Channel.Publish(task.Args);
        }

        public static void Run(IEnumerable<IChannelTask> tasks)
        {
            foreach (var task in tasks.Where(t=>t.Response==null))
            {
                Run(task);
            }
        }
    }
 }
