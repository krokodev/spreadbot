// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// AbstractMipResult.cs

using Spreadbot.Sdk.Common.Crocodev.Common;
using Spreadbot.Sdk.Common.Operations.ResponseResults;
using YamlDotNet.Serialization;

namespace Spreadbot.Core.Channels.Ebay.Services.Mip.Operations.Results
{
    public abstract class AbstractMipResult : IResponseResult
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