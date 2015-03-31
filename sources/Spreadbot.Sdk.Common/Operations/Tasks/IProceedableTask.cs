﻿// Spreadbot (c) 2015 Crocodev
// Spreadbot.Sdk.Common
// IProceedableTask.cs
// Roman, 2015-03-31 1:27 PM

using System.Collections.Generic;

namespace Spreadbot.Sdk.Common.Operations.Tasks
{
    public interface IProceedableTask : IAbstractTask
    {
        IEnumerable< ITaskProceedInfo > GetProceedHistory();
        void AddProceedInfo( ITaskProceedInfo info );
        void AssertCanBeProceeded();
    }
}