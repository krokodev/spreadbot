// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// EbayPublishTask.cs
// romak_000, 2015-03-20 20:46

using Spreadbot.Core.Abstracts.Chanel.Operations.Methods;
using Spreadbot.Core.Abstracts.Chanel.Operations.Tasks;
using Spreadbot.Core.Channels.Ebay.Manager;
using Spreadbot.Core.Channels.Ebay.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Request;
using Spreadbot.Core.Channels.Ebay.Operations.Args;
using Spreadbot.Sdk.Common.Operations.Tasks;

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
            Args = new EbayPublishArgs {
                MipFeedHandler = new MipFeedHandler( mipFeedType ) {
                    Content = feedContent,
                    ItemInfo = itemInfo
                }
            };
            MipRequestStatusCode = MipRequestStatus.Unknown;
        }
    }
}