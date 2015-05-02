// Spreadbot (c) 2015 Krokodev
// Spreadbot.Sdk.Common
// TaskProceedInfo.cs

using System;
using System.Diagnostics.CodeAnalysis;

namespace Spreadbot.Sdk.Common.Operations.Proceed
{
    [SuppressMessage( "ReSharper", "UnusedAutoPropertyAccessor.Global" )]
    [SuppressMessage( "ReSharper", "AutoPropertyCanBeMadeGetOnly.Global" )]
    public class TaskProceedInfo : ITaskProceedInfo
    {
        public TaskProceedInfo()
        {
            Details = "";
            ProceedTime = DateTime.Now;
        }

        public TaskProceedInfo( string details )
            : this()
        {
            Details = details;
        }

        public string Details { get; set; }

        public DateTime ProceedTime { get; set; }
    }
}