// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Stores
// DemoshopStoreManager.pvt.Submit.Amazon.cs

using System.Globalization;
using System.IO;
using Krokodev.Common.Extensions;
using Spreadbot.Core.Abstracts.Channel.Operations.Methods;
using Spreadbot.Core.Channels.Amazon.Adapter;
using Spreadbot.Core.Channels.Amazon.Operations.Args;
using Spreadbot.Core.Channels.Amazon.Operations.Tasks;
using Spreadbot.Core.Channels.Amazon.Services.Mws.Feed;
using Spreadbot.Core.Channels.Amazon.Services.Mws.FeedSubmission;
using Spreadbot.Core.Stores.Demoshop.Configuration.Sections;
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
                    Description = string.Format( "Submit [{0}] to Amazon", Item.Sku )
                };

            storeTask.AddSubTasks(
                _CreateAmazonSubmissionSubTask( MwsFeedType.Product ),
                _CreateAmazonSubmissionSubTask( MwsFeedType.Price ),
                _CreateAmazonSubmissionSubTask( MwsFeedType.Inventory ),
                _CreateAmazonSubmissionSubTask( MwsFeedType.Image )
                );

            _AddTask( storeTask );
            return storeTask;
        }

        // --------------------------------------------------------[]
        private AmazonSubmissionTask _CreateAmazonSubmissionSubTask( MwsFeedType mwsFeedType )
        {
            return new AmazonSubmissionTask {
                IsCritical = true,
                MwsFeedSubmissionResultStatusCode = MwsFeedSubmissionResultStatus.Unknown,
                ChannelId = AmazonAdapter.Instance.Id,
                ChannelMethod = ChannelMethod.Submit,
                Args = new AmazonSubmissionArgs {
                    MwsFeedDescriptor = new MwsFeedDescriptor( mwsFeedType ) {
                        Content = FeedContent( mwsFeedType ),
                        ItemInfo = Item.Sku,
                    },
                }
            };
        }

        // --------------------------------------------------------[]
        private string FeedContent( MwsFeedType mwsFeedType )
        {
            var template = FeedTemplate( mwsFeedType );

            switch( mwsFeedType ) {
                case MwsFeedType.Product :
                    return template
                        .Replace( "{item.sku}", Item.Sku )
                        .Replace( "{item.title}", Item.Title )
                        ;
                case MwsFeedType.Inventory :
                    return template
                        .Replace( "{item.sku}", Item.Sku )
                        .Replace(
                            "{item.quantity}",
                            Item.Quantity.ToString( CultureInfo.CreateSpecificCulture( "en-US" ) ) )
                        ;
                case MwsFeedType.Price :
                    return template
                        .Replace( "{item.sku}", Item.Sku )
                        .Replace( "{item.price}",
                            Item.Price.ToString( CultureInfo.CreateSpecificCulture( "en-US" ) ) )
                        ;
                case MwsFeedType.Image :
                    return template
                        .Replace( "{item.sku}", Item.Sku )
                        .Replace( "{item.price}",
                            Item.Price.ToString( CultureInfo.CreateSpecificCulture( "en-US" ) ) )
                        ;
            }

            throw new SpreadbotException( "Wrong FeedType=[{0}]", mwsFeedType );
        }

        // --------------------------------------------------------[]
        private static string FeedTemplate( MwsFeedType mwsFeedType )
        {
            var templateFolder = DemoshopConfig.Instance.DemoshopPaths.AmazonTemplatesDir.MapPathToDataDirectory();
            var xmlTemplateFilePath = string.Format( @"{0}{1}.xml", templateFolder, mwsFeedType );
            return File.ReadAllText( xmlTemplateFilePath );
        }
    }
}