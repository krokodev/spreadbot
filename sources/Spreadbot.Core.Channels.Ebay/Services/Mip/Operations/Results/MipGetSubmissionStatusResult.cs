// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// MipGetSubmissionStatusResult.cs

using Spreadbot.Core.Channels.Ebay.Services.Mip.Operations.Submission;

namespace Spreadbot.Core.Channels.Ebay.Services.Mip.Operations.Results
{
    public class MipGetSubmissionStatusResult : AbstractMipResult
    {
        public MipSubmissionStatus MipSubmissionStatusCode { get; set; }
        public string MipItemId { get; set; }
        public string Details { get; set; }
    }
}