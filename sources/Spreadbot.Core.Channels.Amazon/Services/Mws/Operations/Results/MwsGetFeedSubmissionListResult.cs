// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Amazon
// MwsGetFeedSubmissionListResult.cs

using System.Collections.Generic;
using Spreadbot.Core.Channels.Amazon.Services.Mws.Operations.FeedSubmission;

namespace Spreadbot.Core.Channels.Amazon.Services.Mws.Operations.Results
{
    public class MwsGetFeedSubmissionListResult : AbstractMwsResponseResult
    {
        public List< MwsFeedSubmissionDescriptor > FeedSubmissionDescriptors { get; set; }
    }
}