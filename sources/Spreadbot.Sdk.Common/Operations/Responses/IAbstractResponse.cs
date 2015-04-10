// Spreadbot (c) 2015 Crocodev
// Spreadbot.Sdk.Common
// IAbstractResponse.cs

namespace Spreadbot.Sdk.Common.Operations.Responses
{
    public interface IAbstractResponse
    {
        bool IsSuccess { get; }
        void Check();
    }
}