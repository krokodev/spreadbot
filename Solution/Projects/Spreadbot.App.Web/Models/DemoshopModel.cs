using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Crocodev.Common.Identifier;
using Spreadbot.Core.Channel.Ebay;
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
        // IStore 
        public static IStore AsStore
        {
            get { return Instance; }
        }

        private readonly IList<IStoreTask> _tasks = new List<IStoreTask>();

        public IStoreTask GetTaskForChannel(IChannel channel)
        {
            return _tasks.FirstOrDefault(t => t.ChannelId == channel.Id);
        }

        public void PublishItemOnEbay()
        {
            _tasks.Add(new DemoshopTask(new EbayChannel()));
        }
    }

    public class DemoshopTask : IStoreTask
    {
        public DemoshopTask(IChannel channel)
        {
            Channel = channel;
        }

        public IChannel Channel { get; set; }

        public Guid ChannelId { get { return Channel.Id; } }
    }
}