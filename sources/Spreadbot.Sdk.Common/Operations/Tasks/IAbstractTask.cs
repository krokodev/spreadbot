// Spreadbot (c) 2015 Crocodev
// Spreadbot.Sdk.Common
// IAbstractTask.cs

using System;
using System.Collections.Generic;
using Spreadbot.Sdk.Common.Operations.Responses;

namespace Spreadbot.Sdk.Common.Operations.Tasks
{
    public interface IAbstractTask
    {
        string GetBriefInfo();
        IAbstractResponse AbstractResponse { get; }
        string Description { get; set; }
        IEnumerable< IAbstractTask > AbstractSubTasks { get; }
        TaskStatus GetStatusCode();
        bool IsCritical { get; set; }
        DateTime CreationTime { get; }
        DateTime LastUpdateTime { get; }
        string Id { get; }
    }
}