// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Amazon
// AmazonSubmissionTask.cs

using System;
using System.Collections.Generic;
using Spreadbot.Core.Abstracts.Channel.Operations.Tasks;
using Spreadbot.Core.Channels.Amazon.Operations.Args;
using Spreadbot.Core.Channels.Amazon.Services.Mws.FeedSubmission;
using Spreadbot.Sdk.Common.Operations.Proceed;
using YamlDotNet.Serialization;

namespace Spreadbot.Core.Channels.Amazon.Operations.Tasks
{
    public sealed partial class AmazonSubmissionTask : AbstractChannelTask, IProceedableTask
    {
        public AmazonSubmissionTask()
        {
            ProceedHistory = new List< ITaskProceedInfo >();
        }

        [YamlMember( Order = 10 )]
        public MwsFeedSubmissionResultStatus MwsFeedSubmissionResultStatusCode { get; set; }

        [YamlMember( Alias = "AmazonSubmissionArgs", Order = 29 )]
        public AmazonSubmissionArgs Args { get; set; }

        public void WasUpdatedNow()
        {
            LastUpdateTime = DateTime.Now;
        }
    }
}