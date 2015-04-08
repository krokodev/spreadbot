// Spreadbot (c) 2015 Crocodev
// Tests.NUnit
// MockHelper.cs
// Roman, 2015-04-07 2:58 PM

using Moq;
using Spreadbot.Core.Abstracts.Store.Manager;
using Spreadbot.Core.Channels.Ebay.Mip.Connector;
using Spreadbot.Core.Channels.Ebay.Mip.SftpHelper;
using Spreadbot.Core.Stores.Demoshop.Manager;

namespace Tests.NUnit.Mocks
{
    internal partial class MockHelper
    {
        /// <summary>
        /// Gets the mip connector using local data.
        /// </summary>
        /// <returns></returns>
        public static IMipConnector GetMipConnectorUsingLocalData()
        {
            var mockMipConnector = new Mock< MipConnector > { CallBase = true };
            var mockSftpHelper = new Mock< WinScpSftpHelper > { CallBase = true };
            mockMipConnector.Object.SftpHelper = mockSftpHelper.Object;

            ConfigureSftpHelperToGetContentFromLocalFolder( mockSftpHelper );

            return mockMipConnector.Object;
        }

        /// <summary>
        /// Gets the mip connector ignoring inprocess and sending test feed.
        /// </summary>
        /// <returns></returns>
        public static IMipConnector GetMipConnectorIgnoringInprocessAndSendingTestFeed()
        {
            var mockMipConnector = new Mock< MipConnector > { CallBase = true };
            var mockSftpHelper = new Mock< WinScpSftpHelper > { CallBase = true };
            mockMipConnector.Object.SftpHelper = mockSftpHelper.Object;

            ConfigureMipConnectorToSendTestFeed( mockMipConnector );
            ConfigureSftpHelperToIgnoreInprocess( mockSftpHelper );

            return mockMipConnector.Object;
        }

        /// <summary>
        /// Gets the mip connector sending test feed.
        /// </summary>
        /// <returns></returns>
        public static IMipConnector GetMipConnectorSendingTestFeed()
        {
            var mockMipConnector = new Mock< MipConnector > { CallBase = true };

            ConfigureMipConnectorToSendTestFeed( mockMipConnector );

            return mockMipConnector.Object;
        }

        /// <summary>
        /// Gets the DemoshopStoreManager creating simple publish on ebay task.
        /// </summary>
        /// <returns></returns>
        public static IStoreManager GetDemoshopStoreManagerCreatingSimplePublishOnEbayTask()
        {
            var mockDemoshopStoreManager = new Mock< DemoshopStoreManager > { CallBase = true };

            ConfigureMipConnectorToCreategSimplePublishOnEbayTask( mockDemoshopStoreManager );

            return mockDemoshopStoreManager.Object;
        }
    }
}