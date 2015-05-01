// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Amazon
// AmazonSubmissionResult.cs

using Spreadbot.Core.Channels.Amazon.Services.Mws.Operations.Results;
using Spreadbot.Sdk.Common.Operations.ResponseResults;

namespace Spreadbot.Core.Channels.Amazon.Operations.Results
{
    public class AmazonSubmissionResult : ResponseResult
    {
        public string MwsRequestId { get; set; }
        public string MwsItemId { get; set; }
    }
}