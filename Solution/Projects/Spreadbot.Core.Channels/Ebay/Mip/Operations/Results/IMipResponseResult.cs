// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// IMipResponseResult.cs
// romak_000, 2015-03-20 13:56

using System;
using Spreadbot.Sdk.Common.Operations.ResponseResults;

namespace Spreadbot.Core.Channels.Ebay.Mip.Operations.Results
{
    public interface IMipResponseResult : IResponseResult
    {
        Guid GetMipRequestId();
    }
}