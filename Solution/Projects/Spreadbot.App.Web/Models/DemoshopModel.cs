using System;
using System.Collections.Generic;
using System.Threading;
using Spreadbot.Core.Channel.Ebay;
using Spreadbot.Core.Channel.Ebay.Mip;
using Spreadbot.Core.System;

namespace Spreadbot.App.Web
{
    // >> | Model | DemoshopModel
    public class DemoshopModel : IStore
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
        private readonly IList<IChannelTask> _tasks = new List<IChannelTask>();
        // --------------------------------------------------------[]
        // Code: DemoshopModel.PublishItemOnEbay
        public void PublishItemOnEbay()
        {
            // Todo: Create Feed XML

            var ebayPublishTask = new EbayPublishTask(FeedType.Product);

            _tasks.Add(ebayPublishTask);
        }

        // ===================================================================================== []
        // IStore
        public IEnumerable<IChannelTask> Tasks
        {
            get { return _tasks; }
        }
    }
}