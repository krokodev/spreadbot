using System.Collections.Generic;

namespace Spreadbot.Sdk.Common
{
    public interface ITask
    {
        string Autoinfo { get; }
        ITaskArgs Args { get; }
        IResponse Response { get; set; }
        string Description { get; set; }
        IEnumerable<ITask> SubTasks { get; }
        TaskStatus StatusCode { get; }
        bool IsCritical { get; set; }
    }
}