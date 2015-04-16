// Spreadbot (c) 2015 Crocodev
// Spreadbot.Tests.NUnit
// MockHelper.pvt.MipConnector.cs

using Moq;
using Spreadbot.Core.Channels.Ebay.Mip.Connector;
using Spreadbot.Core.Channels.Ebay.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Request;

namespace Spreadbot.Tests.NUnit.Mocks
{
    internal partial class MockHelper
    {
        private static void ConfigureMipConnectorToSendTestFeed( Mock< MipConnector > mockMipConnector )
        {
            mockMipConnector
                .Setup(
                    mock => mock.SendFeed( It.IsAny< MipFeedHandler >() ) )
                .Returns(
                    ( MipFeedHandler feedHandler ) =>
                        mockMipConnector.Object.SendFeed( feedHandler, MipRequestHandler.GenerateZeroId() )
                );
        }
    }
}