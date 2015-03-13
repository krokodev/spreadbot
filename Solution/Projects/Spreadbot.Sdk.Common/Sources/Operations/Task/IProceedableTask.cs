namespace Spreadbot.Sdk.Common
{
    public interface IProceedableTask
    {
        void SaveProceedInfo(ITaskProceedInfo info);
        void AssertCanBeProceeded();
    }
}