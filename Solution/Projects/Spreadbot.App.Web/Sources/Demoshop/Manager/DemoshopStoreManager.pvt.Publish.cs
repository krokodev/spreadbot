// Spreadbot (c) 2015 Crocodev
// Spreadbot.App.Web
// DemoshopStore.pvt.Publish.cs
// romak_000, 2015-03-19 17:21

using System.Globalization;
using System.IO;
using Crocodev.Common.Extensions;
using Spreadbot.App.Web.Sources.Configuration.Sections;
using Spreadbot.App.Web.Sources.Demoshop.Task;
using Spreadbot.Core.Connectors.Ebay.Channel.Operations.Tasks;
using Spreadbot.Core.Connectors.Ebay.Mip.Feed;
using Spreadbot.Sdk.Common.Exceptions;

namespace Spreadbot.App.Web.Sources.Demoshop.Store
{
    public partial class DemoshopStoreManager
    {
        // ===================================================================================== []
        // PublishItemOnEbay
        private void DoPublishOnEbay()
        {
            var storeTask =
                new DemoshopStoreTask(Id, "Publish [{0}] on eBay".SafeFormat(Item.Sku));

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

        // ===================================================================================== []
        // FeedContent
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

        // ===================================================================================== []
        // FeedTemplate
        private static string FeedTemplate(MipFeedType mipFeedType)
        {
            var templateFolder = DemoshopConfig.Instance.DemoshopPaths.XmlTemplatesPath.MapPathToDataDirectory();
            var xmlTemplateFilePath = string.Format(@"{0}{1}.xml", templateFolder, mipFeedType);
            return File.ReadAllText(xmlTemplateFilePath);
        }
    }
}