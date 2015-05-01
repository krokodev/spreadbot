// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Amazon
// MwsGetFeedSubmissionsResult.cs

using System.Collections.Generic;
using Spreadbot.Core.Channels.Amazon.Services.Mws.Operations.FeedSubmission;
using Spreadbot.Sdk.Common.Operations.ResponseResults;

namespace Spreadbot.Core.Channels.Amazon.Services.Mws.Operations.Results
{
    public class MwsGetFeedSubmissionsResult : ResponseResult
    {
        public List< MwsFeedSubmissionDescriptor > FeedSubmissionDescriptors { get; set; }
    }
}