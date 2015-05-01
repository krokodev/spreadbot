// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// EbaySubmissionResult.cs

using Spreadbot.Sdk.Common.Operations.ResponseResults;

namespace Spreadbot.Core.Channels.Ebay.Operations.Results
{
    public class EbaySubmissionResult : ResponseResult
    {
        public string MipSubmissionId { get; set; }
        public string MipItemId { get; set; }
    }
}