// Spreadbot (c) 2015 Crocodev
// Spreadbot.Sdk.Common
// IResponse.cs
// romak_000, 2015-03-19 15:49

namespace Spreadbot.Sdk.Common.Operations.Responses
{
    public interface IResponse
    {
        bool IsSuccess { get; }
        string Autoinfo { get; }
        string GetAutoinfo(int level);
        void Check();
    }
}