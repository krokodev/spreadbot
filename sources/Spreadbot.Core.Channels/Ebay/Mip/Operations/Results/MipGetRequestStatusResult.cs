// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// MipGetRequestStatusResult.cs
// Roman, 2015-04-07 2:57 PM

using Spreadbot.Core.Channels.Ebay.Mip.Operations.Request;

namespace Spreadbot.Core.Channels.Ebay.Mip.Operations.Results
{
    public class MipGetRequestStatusResult : AbstractMipResponseResult
    {
        public MipRequestStatus MipRequestStatusCode { get; set; }
        public string MipItemId { get; set; }
        public string Details { get; set; }
    }
}