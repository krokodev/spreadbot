// Spreadbot (c) 2015 Crocodev
// Spreadbot.App.Web
// DemoshopStore.cs
// romak_000, 2015-03-19 15:37

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using Crocodev.Common.Extensions;
using Nereal.Serialization;
using Spreadbot.App.Web.Sources.Configuration.Sections;
using Spreadbot.Core.Channel.Ebay.Channel.Operations.Tasks;
using Spreadbot.Core.Channel.Ebay.Mip.Feed;
using Spreadbot.Core.Common.Channel.Operations.Tasks;
using Spreadbot.Core.Common.Store;
using Spreadbot.Core.Common.Store.Operations;
using Spreadbot.Sdk.Common.Exceptions;

namespace Spreadbot.App.Web.Sources.Demoshop
{
    // !>> App | Web | DemoshopStore
    public class DemoshopStore : IStore
    {
        // ===================================================================================== []
        // Instance
        private static readonly Lazy<DemoshopStore> LazyInstance =
            new Lazy<DemoshopStore>(CreateInstance, LazyThreadSafetyMode.ExecutionAndPublication);

        // --------------------------------------------------------[]
        public DemoshopStore()
        {
            LoadItem();
        }

        // --------------------------------------------------------[]
        private static DemoshopStore CreateInstance()
        {
            return new DemoshopStore();
        }

        // --------------------------------------------------------[]
        public static DemoshopStore Instance
        {
            get { return LazyInstance.Value; }
        }

        // ===================================================================================== []
        // Item
        [NotSerialize]
        public DemoshopItem Item { get; set; }

        // --------------------------------------------------------[]
        private void LoadItem()
        {
            Item = new DemoshopItem()
            {
                Sku = "DS-1001",
                Title = "Demoshop Single Item",
                Price = 7.00m,
                Quantity = 3
            };
        }

        // --------------------------------------------------------[]
        public void SaveItem(DemoshopItem item)
        {
            Item = item;
        }

        // ===================================================================================== []
        // Tasks
        private List<DemoshopStoreTask> _storeTasks = new List<DemoshopStoreTask>();
        // --------------------------------------------------------[]
        private void AddTask(DemoshopStoreTask task)
        {
            _storeTasks.Add(task);
        }

        // --------------------------------------------------------[]
        public List<DemoshopStoreTask> StoreTasks
        {
            get { return _storeTasks; }
            set { _storeTasks = value; }
        }

        // --------------------------------------------------------[]
        public IEnumerable<AbstractChannelTask> GetChannelTasks()
        {
            return StoreTasks.SelectMany(t => t.SubTasks.Select(cnt => (AbstractChannelTask) cnt));
        }

        // ===================================================================================== []
        // PublishItemOnEbay
        public void PublishItemOnEbay()
        {
            var storeTask =
                new DemoshopStoreTask(this, "Publish [{0}] on eBay".SafeFormat(Item.Sku));

            var productTask =
                new EbayPublishTask(MipFeedType.Product, FeedContent(MipFeedType.Product), Item.Sku);
            var distributionTask =
                new EbayPublishTask(MipFeedType.Distribution, FeedContent(MipFeedType.Distribution), Item.Sku);
            var availabilityTask =
                new EbayPublishTask(MipFeedType.Availability, FeedContent(MipFeedType.Availability), Item.Sku);

            storeTask
                .AddSubTask(productTask)
                .AddSubTask(distributionTask)
                .AddSubTask(availabilityTask);

            AddTask(storeTask);
        }

        // --------------------------------------------------------[]
        private static string FeedContent(MipFeedType mipFeedType)
        {
            var template = FeedTemplate(mipFeedType);
            var item = Instance.Item;

            switch (mipFeedType)
            {
                case MipFeedType.Product:
                    return template
                        .Replace("{item.sku}", item.Sku)
                        .Replace("{item.title}", item.Title)
                        ;
                case MipFeedType.Availability:
                    return template
                        .Replace("{item.sku}", item.Sku)
                        .Replace("{item.quantity}", item.Quantity.ToString(CultureInfo.CreateSpecificCulture("en-US")))
                        ;
                case MipFeedType.Distribution:
                    return template
                        .Replace("{item.sku}", item.Sku)
                        .Replace("{item.price}", item.Price.ToString(CultureInfo.CreateSpecificCulture("en-US")))
                        ;
            }

            throw new SpreadbotException("Wrong FeedType=[{0}]", mipFeedType);
        }

        // --------------------------------------------------------[]
        private static string FeedTemplate(MipFeedType mipFeedType)
        {
            var templateFolder = DemoshopConfig.Instance.DemoshopPaths.XmlTemplatesPath.MapPathToDataDirectory();
            var xmlTemplateFilePath = string.Format(@"{0}{1}.xml", templateFolder, mipFeedType);
            return File.ReadAllText(xmlTemplateFilePath);
        }

        // ===================================================================================== []
        // IStore
        public string Name
        {
            get { return "Demoshop"; }
        }

        // --------------------------------------------------------[]
        IEnumerable<IChannelTask> IStore.GetChannelTasks()
        {
            return GetChannelTasks();
        }

        // --------------------------------------------------------[]
        IEnumerable<IStoreTask> IStore.StoreTasks
        {
            get { return StoreTasks; }
        }

        // ===================================================================================== []
        // Save/Restore
        // Code: Demoshop.SaveChanges()
        // --------------------------------------------------------[]
        public void Save()
        {
            Serializer.Default.Serialize(this, DataFileName());
        }

        // --------------------------------------------------------[]
        public void Restore()
        {
            Serializer.Default.Deserialize(this, DataFileName());
        }

        // --------------------------------------------------------[]
        private static string DataFileName()
        {
            return DemoshopConfig.Instance.DemoshopPaths.XmlDataFileName.MapPathToDataDirectory();
        }
    }
}