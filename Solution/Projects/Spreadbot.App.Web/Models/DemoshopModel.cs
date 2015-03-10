using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Crocodev.Common;
using Spreadbot.Core.Channel.Ebay;
using Spreadbot.Core.Channel.Ebay.Mip;
using Spreadbot.Core.Common;
using Spreadbot.Core.System;

namespace Spreadbot.App.Web
{
    // >> Model | DemoshopModel
    public class DemoshopModel : Store
    {
        // ===================================================================================== []
        // Instance
        private static readonly Lazy<DemoshopModel> LazyInstance = new Lazy<DemoshopModel>(CreateInstance,
            LazyThreadSafetyMode.ExecutionAndPublication);

        public DemoshopModel()
        {
            LoadItem();
        }

        // --------------------------------------------------------[]

        private static DemoshopModel CreateInstance()
        {
            return new DemoshopModel();
        }

        // --------------------------------------------------------[]
        public static DemoshopModel Instance
        {
            get { return LazyInstance.Value; }
        }

        // ===================================================================================== []
        // Item
        public DemoshopItemModel Item { get; set; }
        // --------------------------------------------------------[]
        private void LoadItem()
        {
            Item = new DemoshopItemModel()
            {
                Sku = "DS-001",
                Title = "Demoshop Single Item",
                Price = 115.00m,
                Quantity = 3
            };
        }

        // --------------------------------------------------------[]
        public void SaveItem(DemoshopItemModel item)
        {
            Item = item;
        }

        // ===================================================================================== []
        // Tasks
        // Code: Demostore : PublishItemOnEbay
        public void PublishItemOnEbay()
        {
            // Todo: Create Feed XML


            var storeTask = new StoreTask("Publish [{0}] on eBay".SafeFormat(Item));

            storeTask.AddSubTask(new EbayPublishTask(FeedType.Product));
            storeTask.AddSubTask(new EbayPublishTask(FeedType.Availability));
            storeTask.AddSubTask(new EbayPublishTask(FeedType.Distribution));

            AddTask(storeTask, true);
        }
    }
}