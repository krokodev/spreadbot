// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// EbayPublishTask.cs
// romak_000, 2015-03-20 13:56

using System;
using Spreadbot.Core.Abstracts.Chanel.Operations.Methods;
using Spreadbot.Core.Abstracts.Chanel.Operations.Tasks;
using Spreadbot.Core.Channels.Ebay.Manager;
using Spreadbot.Core.Channels.Ebay.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Request;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Response;
using Spreadbot.Core.Channels.Ebay.Operations.Args;
using Spreadbot.Sdk.Common.Operations.Tasks;

// !>> Core | EBay | EbayPublishTask

namespace Spreadbot.Core.Channels.Ebay.Operations.Tasks
{
    public sealed partial class EbayPublishTask : AbstractChannelTask, IProceedableTask
    {
        // ===================================================================================== []
        // Ctor
        public EbayPublishTask() {}

        // --------------------------------------------------------[]
        public EbayPublishTask( MipFeedType mipFeedType, string feedContent, string itemInfo )
            : base( EbayChannelManager.Instance.Id, ChannelMethod.Publish )
        {
            AbstractArgs = new EbayPublishArgs {
                Feed = new MipFeed( mipFeedType ) {
                    Content = feedContent,
                    ItemInfo = itemInfo
                }
            };
            MipRequestStatusCode = MipRequestStatus.Unknown;
        }

        public Guid GetMipRequestId()
        {
            return ( ( IMipResponse ) ( ( ITask ) this ).Response ).GetMipRequestId();
        }
    }
}