// Spreadbot (c) 2015 Krokodev
// Spreadbot.Sdk.Common
// AbstractResponseResult.cs

using Spreadbot.Sdk.Common.Crocodev.Common;

namespace Spreadbot.Sdk.Common.Operations.ResponseResults
{
    public abstract class AbstractResponseResult : IResponseResult
    {
        public override string ToString()
        {
            return this.ToYamlString();
        }
    }
}