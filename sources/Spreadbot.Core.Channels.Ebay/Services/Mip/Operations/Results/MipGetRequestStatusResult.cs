// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// MipGetRequestStatusResult.cs

using Spreadbot.Core.Channels.Ebay.Services.Mip.Operations.Request;

namespace Spreadbot.Core.Channels.Ebay.Services.Mip.Operations.Results
{
    public class MipGetRequestStatusResult : AbstractMipResponseResult
    {
        public MipRequestStatus MipRequestStatusCode { get; set; }
        public string MipItemId { get; set; }
        public string Details { get; set; }
    }
}