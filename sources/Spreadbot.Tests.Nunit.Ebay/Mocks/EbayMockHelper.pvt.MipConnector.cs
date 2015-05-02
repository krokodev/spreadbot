// Spreadbot (c) 2015 Krokodev
// Spreadbot.Tests.Nunit.Ebay
// EbayMockHelper.pvt.MipConnector.cs

using Moq;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Connector;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Services.Mip.FeedSubmission;

namespace Spreadbot.Nunit.Ebay.Mocks
{
    internal partial class EbayMockHelper
    {
        private static void ConfigureMipConnectorToSendTestFeed( Mock< MipConnector > mockMipConnector )
        {
            mockMipConnector
                .Setup(
                    mock => mock.SubmitFeed( It.IsAny< MipFeedDescriptor >() ) )
                .Returns(
                    ( MipFeedDescriptor feedHandler ) =>
                        mockMipConnector.Object.SubmitFeed( feedHandler, MipFeedSubmissionDescriptor.GenerateZeroId() )
                );
        }
    }
}