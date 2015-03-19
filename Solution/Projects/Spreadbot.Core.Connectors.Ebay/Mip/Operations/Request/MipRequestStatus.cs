// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Connectors.Ebay
// MipRequestStatus.cs
// romak_000, 2015-03-19 15:38

namespace Spreadbot.Core.Channel.Ebay.Mip.Operations.Request
{
    public enum MipRequestStatus
    {
        Unknown = 0,
        Initial,
        Inprocess,
        Success,
        Fail
    }
}