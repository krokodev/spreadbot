// Spreadbot (c) 2015 Crocodev
// Spreadbot.Sdk.Common
// IProceedableTask.cs
// romak_000, 2015-03-19 13:44

namespace Spreadbot.Sdk.Common.Operations.Tasks
{
    public interface IProceedableTask
    {
        void SaveProceedInfo(ITaskProceedInfo info);
        void AssertCanBeProceeded();
    }
}