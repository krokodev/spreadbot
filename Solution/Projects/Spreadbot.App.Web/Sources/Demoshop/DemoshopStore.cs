using System;
using System.Globalization;
using System.IO;
using System.Threading;
using Crocodev.Common;
using Spreadbot.App.Web.Configuration;
using Spreadbot.Core.Channel.Ebay;
using Spreadbot.Core.Channel.Ebay.Mip;
using Spreadbot.Core.Common;
using Spreadbot.Core.System;
using Spreadbot.Sdk.Common;

namespace Spreadbot.App.Web
{
    // !>> App | Web | DemoshopStore
    public class DemoshopStore : AbstractStore
    {
        // ===================================================================================== []
        // Instance
        private static readonly Lazy<DemoshopStore> LazyInstance = new Lazy<DemoshopStore>(CreateInstance,
            LazyThreadSafetyMode.ExecutionAndPublication);

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
        public void PublishItemOnEbay()
        {
            var storeTask = new DemoshopStoreTask(this, "Publish [{0}] on eBay".SafeFormat(Item.Sku));

            AddTask(
                storeTask
                    .AddSubTask(new EbayPublishTask(MipFeedType.Product, FeedContent(MipFeedType.Product), Item.Sku))
                    .AddSubTask(new EbayPublishTask(MipFeedType.Availability, FeedContent(MipFeedType.Availability), Item.Sku))
                    .AddSubTask(new EbayPublishTask(MipFeedType.Distribution, FeedContent(MipFeedType.Distribution), Item.Sku)),
                true);
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
                        .Replace("{item.sku}",item.Sku)
                        .Replace("{item.title}",item.Title)
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
        public override string Name
        {
            get { return "Demoshop"; }
        }

    }
}