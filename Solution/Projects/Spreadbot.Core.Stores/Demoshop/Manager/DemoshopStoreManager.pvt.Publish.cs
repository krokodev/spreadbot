// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Stores
// DemoshopStoreManager.pvt.Publish.cs
// romak_000, 2015-03-26 15:05

using System.Globalization;
using System.IO;
using Crocodev.Common.Extensions;
using Spreadbot.Core.Abstracts.Chanel.Operations.Methods;
using Spreadbot.Core.Channels.Ebay.Manager;
using Spreadbot.Core.Channels.Ebay.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Request;
using Spreadbot.Core.Channels.Ebay.Operations.Args;
using Spreadbot.Core.Channels.Ebay.Operations.Tasks;
using Spreadbot.Core.Stores.Demoshop.Configuration.Sections;
using Spreadbot.Core.Stores.Demoshop.Operations.Tasks;
using Spreadbot.Sdk.Common.Exceptions;

namespace Spreadbot.Core.Stores.Demoshop.Manager
{
    public partial class DemoshopStoreManager
    {
        // ===================================================================================== []
        // PublishItemOnEbay
        private DemoshopStoreTask DoCreateTaskPublishOnEbay()
        {
            var storeTask =
                new DemoshopStoreTask {
                    StoreId = Id,
                    Description = string.Format( "Publish [{0}] on eBay", Item.Sku )
                };

            storeTask.AddSubTasks(
                CreateEbayPublishTask( MipFeedType.Product, FeedContent( MipFeedType.Product ), Item.Sku ),
                CreateEbayPublishTask( MipFeedType.Distribution, FeedContent( MipFeedType.Distribution ), Item.Sku ),
                CreateEbayPublishTask( MipFeedType.Availability, FeedContent( MipFeedType.Availability ), Item.Sku )
                );

            AddTask( storeTask );

            return storeTask;
        }

        // --------------------------------------------------------[]
        private static EbayPublishTask CreateEbayPublishTask(
            MipFeedType mipFeedType,
            string feedContent,
            string itemInfo )
        {
            return new EbayPublishTask {
                IsCritical = true,
                MipRequestStatusCode = MipRequestStatus.Unknown,
                ChannelId = EbayChannelManager.Instance.Id,
                ChannelMethod = ChannelMethod.Publish,
                Args = new EbayPublishArgs {
                    MipFeedHandler = new MipFeedHandler( mipFeedType ) {
                        Content = feedContent,
                        ItemInfo = itemInfo,
                    },
                }
            };
        }

        // ===================================================================================== []
        // FeedContent
        private static string FeedContent( MipFeedType mipFeedType )
        {
            var template = FeedTemplate( mipFeedType );
            var item = Instance.Item;

            switch( mipFeedType ) {
                case MipFeedType.Product :
                    return template
                        .Replace( "{item.sku}", item.Sku )
                        .Replace( "{item.title}", item.Title )
                        ;
                case MipFeedType.Availability :
                    return template
                        .Replace( "{item.sku}", item.Sku )
                        .Replace(
                            "{item.quantity}",
                            item.Quantity.ToString( CultureInfo.CreateSpecificCulture( "en-US" ) ) )
                        ;
                case MipFeedType.Distribution :
                    return template
                        .Replace( "{item.sku}", item.Sku )
                        .Replace( "{item.price}",
                            item.Price.ToString( CultureInfo.CreateSpecificCulture( "en-US" ) ) )
                        ;
            }

            throw new SpreadbotException( "Wrong FeedType=[{0}]", mipFeedType );
        }

        // ===================================================================================== []
        // FeedTemplate
        private static string FeedTemplate( MipFeedType mipFeedType )
        {
            var templateFolder = DemoshopConfig.Instance.DemoshopPaths.XmlTemplatesPath.MapPathToDataDirectory();
            var xmlTemplateFilePath = string.Format( @"{0}{1}.xml", templateFolder, mipFeedType );
            return File.ReadAllText( xmlTemplateFilePath );
        }
    }
}