// Spreadbot (c) 2015 Crocodev
// Spreadbot.Sdk.Common
// AbstractResponseResult.cs
// Roman, 2015-04-03 1:45 PM

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