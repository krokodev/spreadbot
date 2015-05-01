// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Amazon
// MwsGetFeedSubmissionListResult.cs

using System.Collections.Generic;
using Spreadbot.Sdk.Common.Operations.ResponseResults;

namespace Spreadbot.Core.Channels.Amazon.Services.Mws.Operations.Results
{
    public class MwsGetFeedSubmissionListResult : IResponseResult {
        public List< string > FeedSubmissionIds { get; set; }
    }
}