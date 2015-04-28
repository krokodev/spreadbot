// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Amazon
// AmazonSubmissionResult.cs

using Spreadbot.Core.Channels.Ebay.Mip.Operations.Results;

namespace Spreadbot.Core.Channels.Amazon.Operations.Results
{
    public class AmazonSubmissionResult : AbstractMipResponseResult
    {
        public string MwsRequestId { get; set; }
        public string MwsItemId { get; set; }
    }
}