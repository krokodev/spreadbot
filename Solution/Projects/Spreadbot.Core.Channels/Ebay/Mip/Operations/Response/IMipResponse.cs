// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// IMipResponse.cs
// romak_000, 2015-03-20 13:56

using System;

namespace Spreadbot.Core.Channels.Ebay.Mip.Operations.Response
{
    public interface IMipResponse
    {
        Guid GetMipRequestId();
    }
}