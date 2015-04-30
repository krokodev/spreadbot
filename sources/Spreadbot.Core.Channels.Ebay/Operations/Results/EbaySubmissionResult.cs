// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// EbaySubmissionResult.cs

using Spreadbot.Core.Channels.Ebay.Services.Mip.Operations.Results;

namespace Spreadbot.Core.Channels.Ebay.Operations.Results
{
    public class EbaySubmissionResult : AbstractMipResult
    {
        public string MipSubmissionId { get; set; }
        public string MipItemId { get; set; }
    }
}