// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// IMipConnector.cs

using Spreadbot.Core.Channels.Ebay.Services.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Operations.Request;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Operations.Response;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Operations.Results;

namespace Spreadbot.Core.Channels.Ebay.Services.Mip.Connector
{
    public interface IMipConnector
    {
        MipResponse< MipSubmitFeedResult > SubmitFeed( MipFeedHandler mipFeedHandler );

        MipResponse< MipFindRequestResult > FindRequest(
            MipRequestHandler mipRequestHandler,
            MipRequestProcessingStage stage );

        MipRequestStatusResponse GetRequestStatus( MipRequestHandler mipRequestHandler );
    }
}