namespace Spreadbot.Core.Common
{
    public interface IResponse
    {
        string Description { get; }
        bool IsSuccess { get; }
        string GetDescription(int level);
    }
}