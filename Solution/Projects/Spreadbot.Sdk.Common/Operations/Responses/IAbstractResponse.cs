// Spreadbot (c) 2015 Crocodev
// Spreadbot.Sdk.Common
// IAbstractResponse.cs
// romak_000, 2015-03-21 2:11

namespace Spreadbot.Sdk.Common.Operations.Responses
{
    public interface IAbstractResponse
    {
        bool IsSuccess { get; }
        string Autoinfo { get; }
        int Level { get; set; }
        void Check();
    }
}