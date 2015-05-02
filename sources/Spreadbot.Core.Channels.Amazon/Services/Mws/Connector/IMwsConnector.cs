// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Amazon
// IMwsConnector.cs

using Spreadbot.Core.Channels.Amazon.Services.Mws.Feed;
using Spreadbot.Core.Channels.Amazon.Services.Mws.Results;
using Spreadbot.Sdk.Common.Operations.Responses;

namespace Spreadbot.Core.Channels.Amazon.Services.Mws.Connector
{
    public interface IMwsConnector
    {
        Response< MwsSubmitFeedResult > SubmitFeed( MwsFeedDescriptor feedDescriptor );
        Response< MwsGetFeedSubmissionsResult > GetFeedSubmissions();
        Response< MwsGetFeedSubmissionCountResult > GetFeedSubmissionCount();
    }
}