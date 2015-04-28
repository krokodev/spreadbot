// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Amazon
// AmazonSubmissionResult.cs

using Spreadbot.Core.Channels.Amazon.Mws.Results;

namespace Spreadbot.Core.Channels.Amazon.Operations.Results
{
    public class AmazonSubmissionResult : AbstractMwsResponseResult
    {
        public string MwsRequestId { get; set; }
        public string MwsItemId { get; set; }
    }
}