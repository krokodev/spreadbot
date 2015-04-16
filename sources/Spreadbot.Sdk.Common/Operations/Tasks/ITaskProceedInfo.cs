// Spreadbot (c) 2015 Krokodev
// Spreadbot.Sdk.Common
// ITaskProceedInfo.cs

using System;

namespace Spreadbot.Sdk.Common.Operations.Tasks
{
    public interface ITaskProceedInfo
    {
        DateTime ProceedTime { get; }
    }
}