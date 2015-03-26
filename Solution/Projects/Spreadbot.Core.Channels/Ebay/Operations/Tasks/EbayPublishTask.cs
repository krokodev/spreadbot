// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// EbayPublishTask.cs
// romak_000, 2015-03-26 19:42

using System;
using System.Collections.Generic;
using Spreadbot.Core.Abstracts.Chanel.Operations.Tasks;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Request;
using Spreadbot.Core.Channels.Ebay.Operations.Args;
using Spreadbot.Sdk.Common.Operations.Tasks;

namespace Spreadbot.Core.Channels.Ebay.Operations.Tasks
{
    public sealed partial class EbayPublishTask : AbstractChannelTask, IProceedableTask
    {
        public EbayPublishTask()
        {
            ProceedHistory = new List< ITaskProceedInfo >();
        }

        public MipRequestStatus MipRequestStatusCode { get; set; }

        public EbayPublishArgs Args { get; set; }

        public void WasUpdatedNow()
        {
            LastUpdateTime = DateTime.Now;
        }
    }
}