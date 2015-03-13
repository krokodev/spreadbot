namespace Spreadbot.Sdk.Common
{
    public interface IResponse
    {
        string Autoinfo { get; }
        string GetAutoinfo(int level);
        void Check();
    }
}