// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// AbstractMipResponseResult.cs
// romak_000, 2015-03-26 19:42

using Spreadbot.Sdk.Common.Crocodev.Common;
using Spreadbot.Sdk.Common.Operations.ResponseResults;

namespace Spreadbot.Core.Channels.Ebay.Mip.Operations.Results
{
    public abstract class AbstractMipResponseResult : IResponseResult
    {
        public override string ToString()
        {
            return this.ToYamlString();
        }
    }
}