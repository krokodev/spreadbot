// Spreadbot (c) 2015 Krokodev
// Spreadbot.Sdk.Common
// TaskProceedInfo.cs

using System;

namespace Spreadbot.Sdk.Common.Operations.Proceed
{
    public class TaskProceedInfo<T> : ITaskProceedInfo
    {
        public TaskProceedInfo( T details )
        {
            Details = details;
            ProceedTime = DateTime.Now;
        }

        public T Details { get; set; }

        public DateTime ProceedTime { get; private set; }
    }
}