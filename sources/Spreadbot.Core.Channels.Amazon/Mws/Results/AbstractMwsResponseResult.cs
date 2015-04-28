// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Amazon
// AbstractMwsResponseResult.cs

using Spreadbot.Sdk.Common.Crocodev.Common;
using Spreadbot.Sdk.Common.Operations.ResponseResults;
using YamlDotNet.Serialization;

namespace Spreadbot.Core.Channels.Amazon.Mws.Results
{
    public abstract class AbstractMwsResponseResult : IResponseResult
    {
        [YamlMember( Order = -1 )]
        public string Type
        {
            get { return GetType().ToString(); }
            set { }
        }

        public override string ToString()
        {
            return this.ToYamlString();
        }
    }
}