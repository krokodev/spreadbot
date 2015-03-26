// Spreadbot (c) 2015 Crocodev
// Spreadbot.Sdk.Common
// IProceedableTask.cs
// romak_000, 2015-03-26 19:42

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