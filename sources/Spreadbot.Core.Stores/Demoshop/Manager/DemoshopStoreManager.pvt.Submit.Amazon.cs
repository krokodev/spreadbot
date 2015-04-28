// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Stores
// DemoshopStoreManager.pvt.Submit.Amazon.cs

using Spreadbot.Core.Abstracts.Channel.Operations.Methods;
using Spreadbot.Core.Channels.Amazon.Mws.Feed;
using Spreadbot.Core.Channels.Amazon.Mws.Operations.Request;
using Spreadbot.Core.Channels.Amazon.Operations.Args;
using Spreadbot.Core.Channels.Amazon.Operations.Tasks;
using Spreadbot.Core.Channels.Ebay.Manager;
using Spreadbot.Core.Channels.Ebay.Mip.Feed;
using Spreadbot.Core.Channels.Ebay.Mip.Operations.Request;
using Spreadbot.Core.Channels.Ebay.Operations.Args;
using Spreadbot.Core.Channels.Ebay.Operations.Tasks;
using Spreadbot.Core.Stores.Demoshop.Operations.Tasks;
using Spreadbot.Sdk.Common.Exceptions;

namespace Spreadbot.Core.Stores.Demoshop.Manager
{
    public partial class DemoshopStoreManager
    {
        // --------------------------------------------------------[]
        private DemoshopStoreTask CreateAmazonSubmissionTask()
        {
            var storeTask =
                new DemoshopStoreTask {
                    StoreId = Id,
                    Description = string.Format( "Submit [{0}] to eBay", Item.Sku )
                };

            storeTask.AddSubTasks(
                _CreateAmazonSubmissionSubTask( MwsFeedType.Product ),
                _CreateAmazonSubmissionSubTask( MwsFeedType.Distribution ),
                _CreateAmazonSubmissionSubTask( MwsFeedType.Availability )
                );

            _AddTask( storeTask );
            return storeTask;
        }

        // --------------------------------------------------------[]
        private AmazonSubmissionTask _CreateAmazonSubmissionSubTask( MwsFeedType mwsFeedType )
        {
            return new AmazonSubmissionTask {
                IsCritical = true,
                MwsRequestStatusCode = MwsRequestStatus.Unknown,
                ChannelId = EbayChannelManager.Instance.Id,
                ChannelMethod = ChannelMethod.Submit,
                Args = new AmazonSubmissionArgs {
                    MwsFeedHandler = new MwsFeedHandler( mwsFeedType ) {
                        Content = FeedContent( mwsFeedType ),
                        ItemInfo = Item.Sku,
                    },
                }
            };
        }

        // --------------------------------------------------------[]
        private string FeedContent( MwsFeedType mwsFeedType )
        {
            /*            var template = FeedTemplate( mipFeedType );

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
            }*/

            throw new SpreadbotException( "Wrong FeedType=[{0}]", mwsFeedType );
        }

        // --------------------------------------------------------[]
        /*
        private static string FeedTemplate( MipFeedType mipFeedType )
        {
            var templateFolder = DemoshopConfig.Instance.DemoshopPaths.XmlTemplatesPath.MapPathToDataDirectory();
            var xmlTemplateFilePath = string.Format( @"{0}{1}.xml", templateFolder, mipFeedType );
            return File.ReadAllText( xmlTemplateFilePath );
        }
*/
    }
}