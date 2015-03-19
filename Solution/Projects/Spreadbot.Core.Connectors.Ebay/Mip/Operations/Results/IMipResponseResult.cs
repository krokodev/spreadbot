// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Connectors.Ebay
// IMipResponseResult.cs
// romak_000, 2015-03-19 15:38

using System;
using Spreadbot.Sdk.Common.Operations.ResponseResults;

namespace Spreadbot.Core.Channel.Ebay.Mip.Operations.Results
{
    public interface IMipResponseResult : IResponseResult
    {
        Guid GetMipRequestId();
    }
}