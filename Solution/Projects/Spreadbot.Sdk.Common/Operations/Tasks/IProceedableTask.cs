// Spreadbot (c) 2015 Crocodev
// Spreadbot.Sdk.Common
// IProceedableTask.cs
// romak_000, 2015-03-21 2:11

namespace Spreadbot.Sdk.Common.Operations.Tasks
{
    public interface IProceedableTask : IAbstractTask
    {
        void AddProceedInfo( ITaskProceedInfo info );
        void AssertCanBeProceeded();
    }
}