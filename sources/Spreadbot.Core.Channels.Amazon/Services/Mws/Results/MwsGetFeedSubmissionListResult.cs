﻿// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Amazon
// MwsGetFeedSubmissionListResult.cs

using System.Collections.Generic;
using Spreadbot.Core.Channels.Amazon.Services.Mws.FeedSubmission;
using Spreadbot.Sdk.Common.Operations.ResponseResults;

namespace Spreadbot.Core.Channels.Amazon.Services.Mws.Results
{
    public class MwsGetFeedSubmissionListResult : ResponseResult
    {
        public List< MwsFeedSubmissionDescriptor > FeedSubmissionDescriptors { get; set; }
    }
}