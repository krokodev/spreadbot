namespace Spreadbot.Sdk.Common
{
    public interface IResponse
    {
        bool IsSuccess { get; }
        string Autoinfo { get; }
        string GetAutoinfo(int level);
        void Check();
    }
}