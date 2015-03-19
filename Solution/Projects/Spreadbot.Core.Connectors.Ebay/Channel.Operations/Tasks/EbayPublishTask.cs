// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Connectors.Ebay
// EbayPublishTask.cs
// romak_000, 2015-03-19 15:37

using System;
using Spreadbot.Core.Channel.Ebay.Channel.Operations.Args;
using Spreadbot.Core.Channel.Ebay.Mip.Feed;
using Spreadbot.Core.Channel.Ebay.Mip.Operations.Request;
using Spreadbot.Core.Channel.Ebay.Mip.Operations.Response;
using Spreadbot.Core.Common.Channel.Operations.Methods;
using Spreadbot.Core.Common.Channel.Operations.Tasks;
using Spreadbot.Sdk.Common.Operations.Tasks;

// !>> Core | EBay | EbayPublishTask

namespace Spreadbot.Core.Channel.Ebay.Channel.Operations.Tasks
{
    public sealed partial class EbayPublishTask : AbstractChannelTask, IProceedableTask
    {
        // ===================================================================================== []
        // Ctor
        public EbayPublishTask()
        {
        }

        // --------------------------------------------------------[]
        public EbayPublishTask(MipFeedType mipFeedType, string feedContent, string itemInfo)
            : base(EbayChannel.Instance, ChannelMethod.Publish)
        {
            AbstractArgs = new EbayPublishArgs
            {
                Feed = new MipFeed(mipFeedType)
                {
                    Content = feedContent,
                    ItemInfo = itemInfo
                }
            };
            MipRequestStatusCode = MipRequestStatus.Unknown;
        }

        public Guid GetMipRequestId()
        {
            return ((IMipResponse) ((ITask) this).Response).GetMipRequestId();
        }
    }
}