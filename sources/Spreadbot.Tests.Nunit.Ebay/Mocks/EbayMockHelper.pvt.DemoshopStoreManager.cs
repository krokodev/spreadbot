// Spreadbot (c) 2015 Krokodev
// Spreadbot.Tests.Nunit.Ebay
// EbayMockHelper.pvt.DemoshopStoreManager.cs

using Moq;
using Spreadbot.Core.Abstracts.Channel.Operations.Methods;
using Spreadbot.Core.Abstracts.Store.Operations.Tasks;
using Spreadbot.Core.Channels.Ebay.Manager;
using Spreadbot.Core.Channels.Ebay.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Request;
using Spreadbot.Core.Channels.Ebay.Operations.Args;
using Spreadbot.Core.Channels.Ebay.Operations.Tasks;
using Spreadbot.Core.Stores.Demoshop.Manager;
using Spreadbot.Core.Stores.Demoshop.Operations.Tasks;

namespace Spreadbot.Nunit.Ebay.Mocks
{
    internal partial class EbayMockHelper
    {
        private static void ConfigureMipConnectorToCreategSimplePublishOnEbayTask(
            Mock< DemoshopStoreManager > mockDemoshopStoreManager )
        {
            var store = mockDemoshopStoreManager.Object;
            mockDemoshopStoreManager
                .Setup(
                    mock => mock.CreateTask( It.Is< StoreTaskType >( type => type == StoreTaskType.PublishOnEbay ) ) )
                .Returns(
                    () => {
                        var storeTask =
                            new DemoshopStoreTask {
                                StoreId = store.Id,
                                Description = "Fake Task"
                            };
                        storeTask.AddSubTasks(
                            new EbayPublishTask {
                                IsCritical = true,
                                MipRequestStatusCode = MipRequestStatus.Unknown,
                                ChannelId = EbayChannelManager.Instance.Id,
                                ChannelMethod = ChannelMethod.Publish,
                                Args = new EbayPublishArgs {
                                    MipFeedHandler = new MipFeedHandler( MipFeedType.None ) {
                                        Content = "Fake Content",
                                        ItemInfo = "Fake Item"
                                    }
                                }
                            } );
                        store.AddTask( storeTask );
                        return storeTask;
                    }
                );
        }
    }
}