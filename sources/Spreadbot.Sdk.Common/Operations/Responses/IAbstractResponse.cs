// Spreadbot (c) 2015 Crocodev
// Spreadbot.Sdk.Common
// IAbstractResponse.cs
// Roman, 2015-04-07 2:58 PM

namespace Spreadbot.Sdk.Common.Operations.Responses
{
    public interface IAbstractResponse
    {
        bool IsSuccess { get; }
        void Check();
    }
}