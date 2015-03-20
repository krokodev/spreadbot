// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Channels
// EbayPublishTask.state.cs
// romak_000, 2015-03-20 13:56

using Spreadbot.Core.Abstracts.Chanel.Operations.Responses;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Request;
using Spreadbot.Core.Channels.Ebay.Operations.Args;
using Spreadbot.Core.Channels.Ebay.Operations.Results;

namespace Spreadbot.Core.Channels.Ebay.Operations.Tasks
{
    public sealed partial class EbayPublishTask
    {
        public MipRequestStatus MipRequestStatusCode { get; set; }
        public EbayPublishArgs Args { get; set; }
        public ChannelResponse<EbayPublishResult> EbayPublishResponse { get; set; }

    }
}