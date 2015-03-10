using System.Collections.Generic;

namespace Spreadbot.Core.Common
{
    public interface ITask
    {
        string Autoinfo { get; }
        IArgs Args { get; }
        IResponse Response { get; set; }
        string Description { get; set; }
        IEnumerable<ITask> SubTasks { get; }
    }
}