// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Ebay
// IMipConnector.cs

using Spreadbot.Core.Channels.Ebay.Services.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Operations.Response;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Operations.Submission;

namespace Spreadbot.Core.Channels.Ebay.Services.Mip.Connector
{
    public interface IMipConnector
    {
        MipSubmitFeedResponse SubmitFeed( MipFeedDescriptor mipFeedDescriptor );

        MipSubmissionStatusResponse GetSubmissionStatus( MipSubmissionDescriptor mipSubmissionDescriptor );
    }
}