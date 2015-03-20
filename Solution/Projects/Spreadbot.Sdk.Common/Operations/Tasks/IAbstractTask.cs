// Spreadbot (c) 2015 Crocodev
// Spreadbot.Sdk.Common
// ITask.cs
// romak_000, 2015-03-20 19:02

using System.Collections.Generic;
using Spreadbot.Sdk.Common.Operations.Responses;

namespace Spreadbot.Sdk.Common.Operations.Tasks
{
    public interface IAbstractTask
    {
        string GetAutoinfo();
        IAbstractResponse AbstractResponse { get; }
        string Description { get; set; }
        IEnumerable< IAbstractTask > AbstractSubTasks { get; }
        TaskStatus GetStatusCode();
        bool IsCritical { get; set; }
    }
}