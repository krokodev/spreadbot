// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// MipRequestStatus.cs
// romak_000, 2015-03-21 2:11

namespace Spreadbot.Core.Channels.Ebay.Mip.Operations.Request
{
    public enum MipRequestStatus
    {
        Unknown = 0,
        Initial,
        Inprocess,
        Success,
        Failure
    }
}