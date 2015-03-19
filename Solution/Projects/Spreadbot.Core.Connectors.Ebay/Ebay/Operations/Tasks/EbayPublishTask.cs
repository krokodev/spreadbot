// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Connectors.Ebay
// EbayPublishTask.cs
// romak_000, 2015-03-19 15:49

using System;
using Spreadbot.Core.Common.Channel.Operations.Methods;
using Spreadbot.Core.Common.Channel.Operations.Tasks;
using Spreadbot.Core.Connectors.Ebay.Channel.Operations.Args;
using Spreadbot.Core.Connectors.Ebay.Mip.Feed;
using Spreadbot.Core.Connectors.Ebay.Mip.Operations.Request;
using Spreadbot.Core.Connectors.Ebay.Mip.Operations.Response;
using Spreadbot.Sdk.Common.Operations.Tasks;

// !>> Core | EBay | EbayPublishTask

namespace Spreadbot.Core.Connectors.Ebay.Channel.Operations.Tasks
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
            : base(EbayChannelManager.Instance.Id, ChannelMethod.Publish)
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