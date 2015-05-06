// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// IMipConnector.cs

using Spreadbot.Core.Channels.Ebay.Services.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Services.Mip.FeedSubmission;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Results;
using Spreadbot.Sdk.Common.Operations.Responses;

namespace Spreadbot.Core.Channels.Ebay.Services.Mip.Connector
{
    public interface IMipConnector
    {
        Response< MipSubmitFeedResult > SubmitFeed( MipFeedDescriptor mipFeedDescriptor );

        Response< MipGetFeedSubmissionStatusResult > GetFeedSubmissionStatus(
            MipFeedSubmissionDescriptor mipFeedSubmissionDescriptor );
    }
}