// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// MipRequestStatus.cs

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