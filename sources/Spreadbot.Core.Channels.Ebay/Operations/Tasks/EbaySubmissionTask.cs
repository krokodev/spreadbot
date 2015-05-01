// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// EbaySubmissionTask.cs

using System;
using System.Collections.Generic;
using Spreadbot.Core.Abstracts.Channel.Operations.Tasks;
using Spreadbot.Core.Channels.Ebay.Operations.Args;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Operations.FeedSubmission;
using Spreadbot.Sdk.Common.Operations.Tasks;
using YamlDotNet.Serialization;

namespace Spreadbot.Core.Channels.Ebay.Operations.Tasks
{
    public sealed partial class EbaySubmissionTask : AbstractChannelTask, IProceedableTask
    {
        public EbaySubmissionTask()
        {
            ProceedHistory = new List< ITaskProceedInfo >();
        }

        [YamlMember( Order = 10 )]
        public MipFeedSubmissionResultStatus MipFeedSubmissionResultStatusCode { get; set; }

        [YamlMember( Alias = "EbaySubmissionArgs", Order = 29 )]
        public EbaySubmissionArgs Args { get; set; }

        public void WasUpdatedNow()
        {
            LastUpdateTime = DateTime.Now;
        }
    }
}