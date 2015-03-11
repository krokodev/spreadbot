using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using Crocodev.Common;
using Spreadbot.App.Web.Configuration;
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
        public void PublishItemOnEbay()
        {
            var storeTask = new StoreTask("Publish [{0}] on eBay".SafeFormat(Item));

            AddTask(
                storeTask
                    .AddSubTask(new EbayPublishTask(FeedType.Product, FeedContent(FeedType.Product)))
                    .AddSubTask(new EbayPublishTask(FeedType.Availability, FeedContent(FeedType.Availability)))
                    .AddSubTask(new EbayPublishTask(FeedType.Distribution, FeedContent(FeedType.Distribution))),
                true);
        }

        // --------------------------------------------------------[]
        // Code: * Demoshop : FeedContent
        private static string FeedContent(FeedType feedType)
        {
            var template = FeedTemplate(feedType);
            var item = Instance.Item;

            switch (feedType)
            {
                case FeedType.Product:
                    return template
                        .Replace("{item.sku}",item.Sku)
                        .Replace("{item.title}",item.Title)
                        ;
                case FeedType.Availability:
                    return template
                        .Replace("{item.sku}", item.Sku)
                        .Replace("{item.quantity}", item.Quantity.ToString(CultureInfo.CreateSpecificCulture("en-US")))
                        ;
                case FeedType.Distribution:
                    return template
                        .Replace("{item.sku}", item.Sku)
                        .Replace("{item.price}", item.Price.ToString(CultureInfo.CreateSpecificCulture("en-US")))
                        ;
            }

            throw new SpreadbotException("Wrong FeedType=[{0}]", feedType);
        }

        // --------------------------------------------------------[]
        private static string FeedTemplate(FeedType feedType)
        {
            var templateFolder = DemoshopConfig.Instance.Paths.XmlTemplatesPath.MapPathToDataDirectory();
            var xmlTemplateFilePath = string.Format(@"{0}{1}.xml", templateFolder, feedType);
            return File.ReadAllText(xmlTemplateFilePath);
        }
    }
}