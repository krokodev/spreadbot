// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// MipRequestStatus.cs
// Roman, 2015-04-10 1:29 PM

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