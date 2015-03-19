// Spreadbot (c) 2015 Crocodev
// Spreadbot.Sdk.Common
// ITask.cs
// romak_000, 2015-03-19 15:49

using System.Collections.Generic;
using Spreadbot.Sdk.Common.Operations.Args;
using Spreadbot.Sdk.Common.Operations.Responses;

namespace Spreadbot.Sdk.Common.Operations.Tasks
{
    public interface ITask
    {
        string Autoinfo { get; }
        ITaskArgs Args { get; }
        IResponse Response { get; set; }
        string Description { get; set; }
        IEnumerable<ITask> SubTasks { get; }
        TaskStatus GetStatusCode();
        bool IsCritical { get; set; }
    }
}