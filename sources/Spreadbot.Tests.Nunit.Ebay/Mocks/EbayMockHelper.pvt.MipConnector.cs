// Spreadbot (c) 2015 Krokodev
// Spreadbot.Tests.Nunit.Ebay
// EbayMockHelper.pvt.MipConnector.cs

using Moq;
using Spreadbot.Core.Channels.Ebay.Mip.Connector;
using Spreadbot.Core.Channels.Ebay.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Request;

namespace Spreadbot.Nunit.Ebay.Mocks
{
    internal partial class EbayMockHelper
    {
        private static void ConfigureMipConnectorToSendTestFeed( Mock< MipConnector > mockMipConnector )
        {
            mockMipConnector
                .Setup(
                    mock => mock.SubmitFeed( It.IsAny< MipFeedHandler >() ) )
                .Returns(
                    ( MipFeedHandler feedHandler ) =>
                        mockMipConnector.Object.SubmitFeed( feedHandler, MipRequestHandler.GenerateZeroId() )
                );
        }
    }
}