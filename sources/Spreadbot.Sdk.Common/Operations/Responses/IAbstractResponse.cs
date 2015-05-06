// Spreadbot (c) 2015 Krokodev
// Spreadbot.Sdk.Common
// IAbstractResponse.cs

namespace Spreadbot.Sdk.Common.Operations.Responses
{
    public interface IAbstractResponse
    {
        bool IsSuccessful { get; }
    }
}