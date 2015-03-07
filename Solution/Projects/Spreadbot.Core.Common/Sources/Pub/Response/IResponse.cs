namespace Spreadbot.Core.Common
{
    public interface IResponse
    {
        string Description { get; }
        string GetDescription(int level);
    }
}