// Spreadbot (c) 2015 Krokodev
// Spreadbot.Tests.Nunit.Ebay
// EbayMockHelper.pvt.DemoshopStoreManager.cs

using Moq;
using Spreadbot.Core.Abstracts.Channel.Operations.Methods;
using Spreadbot.Core.Abstracts.Store.Operations.Tasks;
using Spreadbot.Core.Channels.Ebay.Adapter;
using Spreadbot.Core.Channels.Ebay.Operations.Args;
using Spreadbot.Core.Channels.Ebay.Operations.Tasks;
using Spreadbot.Core.Channels.Ebay.Services.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Services.Mip.FeedSubmission;
using Spreadbot.Core.Stores.Demoshop.Manager;
using Spreadbot.Core.Stores.Demoshop.Operations.Tasks;

namespace Spreadbot.Nunit.Ebay.Mocks
{
    internal partial class EbayMockHelper
    {
        private static void ConfigureMipConnectorToCreateSimpleSubmitToEbayTask(
            Mock< DemoshopStoreManager > mockDemoshopStoreManager )
        {
            var store = mockDemoshopStoreManager.Object;
            mockDemoshopStoreManager
                .Setup(
                    mock => mock.CreateTask( It.Is< StoreTaskType >( type => type == StoreTaskType.SubmitToEbay ) ) )
                .Returns(
                    () => {
                        var storeTask =
                            new DemoshopStoreTask {
                                StoreId = store.Id,
                                Description = "Fake Task"
                            };
                        storeTask.AddSubTasks(
                            new EbaySubmissionTask {
                                IsCritical = true,
                                MipFeedSubmissionOverallStatus = MipFeedSubmissionOverallStatus.Unknown,
                                ChannelId = EbayAdapter.Instance.Id,
                                ChannelMethod = ChannelMethod.Submit,
                                Args = new EbaySubmissionArgs {
                                    MipFeedDescriptor = new MipFeedDescriptor( MipFeedType.None ) {
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