// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Stores
// DemoshopStoreManager.pvt.Submit.Ebay.cs

using System.Globalization;
using System.IO;
using Krokodev.Common.Extensions;
using Spreadbot.Core.Abstracts.Channel.Operations.Methods;
using Spreadbot.Core.Channels.Ebay.Adapter;
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
        private DemoshopStoreTask CreateEbaySubmissionTask()
        {
            var storeTask =
                new DemoshopStoreTask {
                    StoreId = Id,
                    Description = string.Format( "Submit [{0}] to eBay", Item.Sku )
                };

            storeTask.AddSubTasks(
                _CreateEbaySubmissionSubTask( MipFeedType.Product ),
                _CreateEbaySubmissionSubTask( MipFeedType.Distribution ),
                _CreateEbaySubmissionSubTask( MipFeedType.Availability )
                );

            _AddTask( storeTask );
            return storeTask;
        }

        // --------------------------------------------------------[]
        private EbaySubmissionTask _CreateEbaySubmissionSubTask(
            MipFeedType mipFeedType )
        {
            return new EbaySubmissionTask {
                IsCritical = true,
                MipRequestStatusCode = MipRequestStatus.Unknown,
                ChannelId = EbayAdapter.Instance.Id,
                ChannelMethod = ChannelMethod.Submit,
                Args = new EbaySubmissionArgs {
                    MwsFeedHandler = new MipFeedHandler( mipFeedType ) {
                        Content = FeedContent( mipFeedType ),
                        ItemInfo = Item.Sku,
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
            var templateFolder = DemoshopConfig.Instance.DemoshopPaths.EbayTemplatesDir.MapPathToDataDirectory();
            var xmlTemplateFilePath = string.Format( @"{0}{1}.xml", templateFolder, mipFeedType );
            return File.ReadAllText( xmlTemplateFilePath );
        }
    }
}