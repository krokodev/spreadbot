// Spreadbot (c) 2015 Crocodev
// Spreadbot.Sdk.Common
// ITaskProceedInfo.cs
// romak_000, 2015-03-25 15:25

using System;

namespace Spreadbot.Sdk.Common.Operations.Tasks
{
    public interface ITaskProceedInfo
    {
        DateTime ProceedTime { get; }
    }
}