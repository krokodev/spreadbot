// Spreadbot (c) 2015 Krokodev
// Spreadbot.Sdk.Common
// IProceedableTask.cs

using System.Collections.Generic;
using Spreadbot.Sdk.Common.Operations.Tasks;

namespace Spreadbot.Sdk.Common.Operations.Proceed
{
    public interface IProceedableTask : IAbstractTask
    {
        IEnumerable< ITaskProceedInfo > GetProceedHistory();
        void AddProceedInfo( ITaskProceedInfo info );
        void AssertCanBeProceeded();
    }
}