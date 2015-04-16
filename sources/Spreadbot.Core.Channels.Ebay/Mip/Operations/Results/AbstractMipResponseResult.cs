// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels.Ebay
// AbstractMipResponseResult.cs

using Spreadbot.Sdk.Common.Crocodev.Common;
using Spreadbot.Sdk.Common.Operations.ResponseResults;
using YamlDotNet.Serialization;

namespace Spreadbot.Core.Channels.Ebay.Mip.Operations.Results
{
    public abstract class AbstractMipResponseResult : IResponseResult
    {
        [YamlMember( Order = -1 )]
        public string Type
        {
            get { return GetType().ToString(); }

            // ReSharper disable once ValueParameterNotUsed
            set { }
        }

        public override string ToString()
        {
            return this.ToYamlString();
        }
    }
}