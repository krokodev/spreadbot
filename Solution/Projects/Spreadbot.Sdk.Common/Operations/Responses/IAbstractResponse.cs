// Spreadbot (c) 2015 Crocodev
// Spreadbot.Sdk.Common
// IAbstractResponse.cs
// romak_000, 2015-03-26 19:42

namespace Spreadbot.Sdk.Common.Operations.Responses
{
    public interface IAbstractResponse
    {
        bool IsSuccess { get; }
        void Check();
    }
}