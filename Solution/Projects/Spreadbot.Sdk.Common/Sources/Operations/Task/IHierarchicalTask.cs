using System.Collections.Generic;

namespace Spreadbot.Sdk.Common
{
    public interface IHierarchicalTask
    {
        string Autoinfo { get; }
        ITaskArgs Args { get; }
        IResponse Response { get; set; }
        string Description { get; set; }
        IEnumerable<IHierarchicalTask> SubTasks { get; }
        TaskStatus GetStatusCode();
        bool IsCritical { get; set; }
    }
}