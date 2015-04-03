// Spreadbot (c) 2015 Crocodev
// Spreadbot.Sdk.Common
// ITaskProceedInfo.cs
// Roman, 2015-04-03 1:46 PM

using System;

namespace Spreadbot.Sdk.Common.Operations.Tasks
{
    public interface ITaskProceedInfo
    {
        DateTime ProceedTime { get; }
    }
}