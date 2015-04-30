// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Amazon
// IMwsConnector.cs

using Spreadbot.Core.Channels.Amazon.Services.Mws.Feed;
using Spreadbot.Core.Channels.Amazon.Services.Mws.Operations.Response;
using Spreadbot.Core.Channels.Amazon.Services.Mws.Operations.Results;

namespace Spreadbot.Core.Channels.Amazon.Services.Mws.Connector
{
    public interface IMwsConnector
    {
        MwsResponse< MwsSubmitFeedResult > SubmitFeed( MwsFeedHandler mwsFeedHandler );
    }
}