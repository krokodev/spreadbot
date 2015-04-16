// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// IMipConnector.cs

using Spreadbot.Core.Channels.Ebay.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Request;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Response;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Results;

namespace Spreadbot.Core.Channels.Ebay.Mip.Connector
{
    public interface IMipConnector
    {
        MipResponse< MipSendFeedResult > SendFeed( MipFeedHandler mipFeedHandler );

        MipResponse< MipFindRequestResult > FindRequest(
            MipRequestHandler mipRequestHandler,
            MipRequestProcessingStage stage );

        MipRequestStatusResponse GetRequestStatus( MipRequestHandler mipRequestHandler );
    }
}