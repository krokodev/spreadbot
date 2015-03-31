// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// EbayPublishTask.cs
// Roman, 2015-03-31 1:27 PM

using System;
using System.Collections.Generic;
using Spreadbot.Core.Abstracts.Channel.Operations.Tasks;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Request;
using Spreadbot.Core.Channels.Ebay.Operations.Args;
using Spreadbot.Sdk.Common.Operations.Tasks;
using YamlDotNet.Serialization;

namespace Spreadbot.Core.Channels.Ebay.Operations.Tasks
{
    public sealed partial class EbayPublishTask : AbstractChannelTask, IProceedableTask
    {
        public EbayPublishTask()
        {
            ProceedHistory = new List< ITaskProceedInfo >();
        }

        [YamlMember( Order = 10 )]
        public MipRequestStatus MipRequestStatusCode { get; set; }

        [YamlMember( Alias = "EbayPublishArgs", Order = 29 )]
        public EbayPublishArgs Args { get; set; }

        public void WasUpdatedNow()
        {
            LastUpdateTime = DateTime.Now;
        }
    }
}