// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// EbaySubmissionResult.cs

using Spreadbot.Core.Channels.Ebay.Mip.Operations.Results;

namespace Spreadbot.Core.Channels.Ebay.Operations.Results
{
    public class EbaySubmissionResult : AbstractMipResponseResult
    {
        public string MipRequestId { get; set; }
        public string MipItemId { get; set; }
    }
}