// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// AbstractMipResult.cs

using System.Diagnostics.CodeAnalysis;
using Spreadbot.Sdk.Common.Crocodev.Common;
using YamlDotNet.Serialization;

namespace Spreadbot.Sdk.Common.Operations.ResponseResults
{
    [SuppressMessage( "ReSharper", "ValueParameterNotUsed" )]
    public abstract class ResponseResult : IResponseResult
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