using System;
using Crocodev.Common.Identifier;
using Spreadbot.Core.Channel.Ebay.Mip;
using Spreadbot.Core.Common;
using Spreadbot.Sdk.Common;

// !>> Core | EBay | EbayPublishTask

namespace Spreadbot.Core.Channel.Ebay
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

        public MipRequest.Identifier GetMipRequestId()
        {
            return ((IMipResponse) ((IHierarchicalTask) this).Response).GetMipRequestId();
        }
    }

}