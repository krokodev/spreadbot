// Spreadbot (c) 2015 Crocodev
// Spreadbot.Core.Stores
// DemoshopStoreManager.pvt.Publish.cs
// romak_000, 2015-03-20 13:57

using System.Globalization;
using System.IO;
using Crocodev.Common.Extensions;
using Spreadbot.Core.Channels.Ebay.Mip.Feed;
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
        private DemoshopStoreTask DoPublishOnEbay()
        {
            var storeTask =
                new DemoshopStoreTask( Id, "Publish [{0}] on eBay".SafeFormat( Item.Sku ) );

            var productTask =
                new EbayPublishTask( MipFeedType.Product, FeedContent( MipFeedType.Product ), Item.Sku );
            var distributionTask =
                new EbayPublishTask( MipFeedType.Distribution, FeedContent( MipFeedType.Distribution ), Item.Sku );
            var availabilityTask =
                new EbayPublishTask( MipFeedType.Availability, FeedContent( MipFeedType.Availability ), Item.Sku );

            storeTask
                .AddSubTask( productTask )
                .AddSubTask( distributionTask )
                .AddSubTask( availabilityTask );

            AddTask( storeTask );
            return storeTask;
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
                        .Replace( "{item.price}", item.Price.ToString( CultureInfo.CreateSpecificCulture( "en-US" ) ) )
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