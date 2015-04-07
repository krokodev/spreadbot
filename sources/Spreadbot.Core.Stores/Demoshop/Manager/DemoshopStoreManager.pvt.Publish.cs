// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Stores
// DemoshopStoreManager.pvt.Publish.cs
// Roman, 2015-04-07 12:23 PM

using System.Globalization;
using System.IO;
using Crocodev.Common.Extensions;
using Spreadbot.Core.Abstracts.Channel.Operations.Methods;
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
        // --------------------------------------------------------[]
        private DemoshopStoreTask CreateTaskPublishOnEbay()
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

        // --------------------------------------------------------[]
        private string FeedContent( MipFeedType mipFeedType )
        {
            var template = FeedTemplate( mipFeedType );

            switch( mipFeedType ) {
                case MipFeedType.Product :
                    return template
                        .Replace( "{item.sku}", Item.Sku )
                        .Replace( "{item.title}", Item.Title )
                        ;
                case MipFeedType.Availability :
                    return template
                        .Replace( "{item.sku}", Item.Sku )
                        .Replace(
                            "{item.quantity}",
                            Item.Quantity.ToString( CultureInfo.CreateSpecificCulture( "en-US" ) ) )
                        ;
                case MipFeedType.Distribution :
                    return template
                        .Replace( "{item.sku}", Item.Sku )
                        .Replace( "{item.price}",
                            Item.Price.ToString( CultureInfo.CreateSpecificCulture( "en-US" ) ) )
                        ;
            }

            throw new SpreadbotException( "Wrong FeedType=[{0}]", mipFeedType );
        }

        // --------------------------------------------------------[]
        private static string FeedTemplate( MipFeedType mipFeedType )
        {
            var templateFolder = DemoshopConfig.Instance.DemoshopPaths.XmlTemplatesPath.MapPathToDataDirectory();
            var xmlTemplateFilePath = string.Format( @"{0}{1}.xml", templateFolder, mipFeedType );
            return File.ReadAllText( xmlTemplateFilePath );
        }
    }
}