// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Connectors.Ebay
// IMipResponse.cs
// romak_000, 2015-03-19 14:02

using System;

namespace Spreadbot.Core.Channel.Ebay.Mip.Operations.Response
{
    public interface IMipResponse
    {
        Guid GetMipRequestId();
    }
}