// Spreadbot (c) 2015 Krokodev
// Spreadbot.Tests.Nunit.Ebay
// EbayMockHelper.cs

using Moq;
using Spreadbot.Core.Abstracts.Store.Manager;
using Spreadbot.Core.Channels.Ebay.Mip.Connector;
using Spreadbot.Core.Channels.Ebay.Mip.SftpHelper;
using Spreadbot.Core.Stores.Demoshop.Manager;

namespace Spreadbot.Nunit.Ebay.Mocks
{
    internal partial class EbayMockHelper
    {
        public static IMipConnector GetMipConnectorUsingLocalData()
        {
            var mockMipConnector = new Mock< MipConnector > { CallBase = true };
            var mockSftpHelper = new Mock< WinScpSftpHelper > { CallBase = true };
            mockMipConnector.Object.SftpHelper = mockSftpHelper.Object;

            ConfigureSftpHelperToGetContentFromLocalFolder( mockSftpHelper );

            return mockMipConnector.Object;
        }

        public static IMipConnector GetMipConnectorIgnoringInprocessAndSendingTestFeed()
        {
            var mockMipConnector = new Mock< MipConnector > { CallBase = true };
            var mockSftpHelper = new Mock< WinScpSftpHelper > { CallBase = true };
            mockMipConnector.Object.SftpHelper = mockSftpHelper.Object;

            ConfigureMipConnectorToSendTestFeed( mockMipConnector );
            ConfigureSftpHelperToIgnoreInprocess( mockSftpHelper );

            return mockMipConnector.Object;
        }

        public static IMipConnector GetMipConnectorSendingTestFeed()
        {
            var mockMipConnector = new Mock< MipConnector > { CallBase = true };

            ConfigureMipConnectorToSendTestFeed( mockMipConnector );

            return mockMipConnector.Object;
        }

        public static IStoreManager GetDemoshopStoreManagerCreatingSimpleSubmitToEbayTask()
        {
            var mockDemoshopStoreManager = new Mock< DemoshopStoreManager > { CallBase = true };

            ConfigureMipConnectorToCreateSimpleSubmitToEbayTask( mockDemoshopStoreManager );

            return mockDemoshopStoreManager.Object;
        }
    }
}