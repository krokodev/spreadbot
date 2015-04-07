// Spreadbot (c) 2015 Crocodev
// Tests.NUnit
// MockHelper.pvt.MipConnector.cs
// Roman, 2015-04-07 2:58 PM

using Moq;
using Spreadbot.Core.Channels.Ebay.Mip.Connector;
using Spreadbot.Core.Channels.Ebay.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Request;

namespace Tests.NUnit.Mocks
{
    internal partial class MockHelper
    {
        // --------------------------------------------------------[]
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