// Spreadbot (c) 2015 Crocodev
// Spreadbot.Sdk.Common
// AbstractResponseResult.cs
// romak_000, 2015-03-25 15:25

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