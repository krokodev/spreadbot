// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Amazon
// IMwsConnector.cs

using Spreadbot.Core.Channels.Amazon.Services.Mws.Feed;
using Spreadbot.Core.Channels.Amazon.Services.Mws.Operations.Response;

namespace Spreadbot.Core.Channels.Amazon.Services.Mws.Connector
{
    public interface IMwsConnector
    {
        MwsSubmitFeedResponse SubmitFeed( MwsFeedDescriptor feedDescriptor );
        MwsGetFeedSubmissionListResponse GetFeedSubmissionList();
    }
}