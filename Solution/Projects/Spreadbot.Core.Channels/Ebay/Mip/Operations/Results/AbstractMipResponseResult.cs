﻿// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// AbstractMipResponseResult.cs
// romak_000, 2015-03-26 20:12

using Spreadbot.Sdk.Common.Crocodev.Common;
using Spreadbot.Sdk.Common.Operations.ResponseResults;
using YamlDotNet.Serialization;

namespace Spreadbot.Core.Channels.Ebay.Mip.Operations.Results
{
    public abstract class AbstractMipResponseResult : IResponseResult
    {
        [YamlMember(Order=-1)]
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