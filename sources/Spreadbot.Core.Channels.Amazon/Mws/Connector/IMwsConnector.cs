// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Amazon
// IMwsConnector.cs

using Spreadbot.Core.Channels.Amazon.Mws.Feed;
using Spreadbot.Core.Channels.Amazon.Mws.Operations.Response;
using Spreadbot.Core.Channels.Amazon.Mws.Results;

namespace Spreadbot.Core.Channels.Amazon.Mws.Connector
{
    public interface IMwsConnector
    {
        MwsResponse< MwsSubmitFeedResult > SubmitFeed( MwsFeedHandler mwsFeedHandler );
    }
}