namespace Spreadbot.Core.Common
{
    public interface IResponse
    {
        string Autoinfo { get; }
        string GetAutoinfo(int level);
    }
}