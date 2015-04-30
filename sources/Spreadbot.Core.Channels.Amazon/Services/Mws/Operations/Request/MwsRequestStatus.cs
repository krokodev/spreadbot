// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Amazon
// MwsRequestStatus.cs

namespace Spreadbot.Core.Channels.Amazon.Services.Mws.Operations.Request
{
    public enum MwsRequestStatus
    {
        Unknown = 0,
        Initial,
        Inprocess,
        Success,
        Failure
    }
}