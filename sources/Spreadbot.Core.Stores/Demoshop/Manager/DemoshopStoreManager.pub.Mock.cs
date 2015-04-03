// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Stores
// DemoshopStoreManager.pub.Mock.cs
// Roman, 2015-04-03 8:16 PM

using System;
using Spreadbot.Core.Channels.Ebay.Mip.Feed;
using Spreadbot.Core.Stores.Demoshop.Operations.Tasks;

// Ref: Use Mock<DemoshopStoreManager>.CreateTask

namespace Spreadbot.Core.Stores.Demoshop.Manager
{
    public partial class DemoshopStoreManager
    {
        // --------------------------------------------------------[]
        public DemoshopStoreTask Mock_CreateTask( DemoshopStoreTaskType taskType )
        {
            switch( taskType ) {
                case DemoshopStoreTaskType.PublishOnEbay :
                    return Mock_DoCreateTaskPublishOnEbay();
            }
            throw new ArgumentException( string.Format( "Unknown taskType: [{0}]", taskType ) );
        }

        // --------------------------------------------------------[]
        private DemoshopStoreTask Mock_DoCreateTaskPublishOnEbay()
        {
            var storeTask =
                new DemoshopStoreTask {
                    StoreId = Id,
                    Description = string.Format( "Publish [{0}] on eBay", Item.Sku )
                };

            storeTask.AddSubTasks(
                CreateEbayPublishTask( MipFeedType.None, FeedContent( MipFeedType.Product ), Item.Sku )
                );

            AddTask( storeTask );

            return storeTask;
        }
    }
}