// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Amazon
// MwsConnector.pvt.Feed.UItils.Convertors.cs

using System.Collections.Generic;
using System.Linq;
using MarketplaceWebService.Model;
using Spreadbot.Core.Channels.Amazon.Services.Mws.Feed;
using Spreadbot.Core.Channels.Amazon.Services.Mws.FeedSubmission;

namespace Spreadbot.Core.Channels.Amazon.Services.Mws.Connector
{
    public partial class MwsConnector
    {
        private const string FeedContentEncoding = "ISO-8859-1";
        private const int GetFeedSubmissionsChunkSize = 100;

        private static readonly Dictionary< MwsFeedType, string > FeedTypeMap =
            new Dictionary< MwsFeedType, string > {
                { MwsFeedType.None, "_NONE_" },
                { MwsFeedType.Image, "_POST_PRODUCT_IMAGE_DATA_" },
                { MwsFeedType.Product, "_POST_PRODUCT_DATA_" },
                { MwsFeedType.Inventory, "_POST_INVENTORY_AVAILABILITY_DATA_" },
                { MwsFeedType.Price, "_POST_PRODUCT_PRICING_DATA_" }
            };

        private static readonly Dictionary< MwsFeedSubmissionProcessingStatus, List< string > >
            FeedSubmissionProcessingStatusMap =
                new Dictionary< MwsFeedSubmissionProcessingStatus, List< string > > {
                    {
                        MwsFeedSubmissionProcessingStatus.Complete, new List< string > {
                            "_DONE_"
                        }
                    }, {
                        MwsFeedSubmissionProcessingStatus.Cancelled, new List< string > {
                            "_CANCELLED_"
                        }
                    }, {
                        MwsFeedSubmissionProcessingStatus.InProgress, new List< string > {
                            "_IN_PROGRESS_",
                            "_SUBMITTED_",
                            "_AWAITING_ASYNCHRONOUS_REPLY_",
                            "_IN_SAFETY_NET_",
                            "_UNCONFIRMED_",
                        }
                    },
                };

        private static StatusList ConvertToNativeStatusList(
            List< MwsFeedSubmissionProcessingStatus > processingStatusList )
        {
            var nativeStatusList = new StatusList();
            processingStatusList.ForEach(
                status => {
                    FeedSubmissionProcessingStatusMap[ status ].ForEach(
                        mwsStatus => { nativeStatusList.WithStatus( mwsStatus ); } );
                } );
            return nativeStatusList;
        }

        private static MwsFeedSubmissionProcessingStatus ConvertToFeedSubmissionProcessingStatus( string nativeStatus )
        {
            return FeedSubmissionProcessingStatusMap
                .Keys.FirstOrDefault( key =>
                    FeedSubmissionProcessingStatusMap[ key ]
                        .Any( s => s == nativeStatus ) );
        }

        private static IdList ConvertToNativeIdList( List< string > feedSubmissionIds )
        {
            var nativeIdList = new IdList();
            nativeIdList.WithId( feedSubmissionIds.ToArray() );
            return nativeIdList;
        }

        private static TypeList ConvertToNativeTypeList( List< MwsFeedType > feedTypeList )
        {
            var nativeTypeList = new TypeList();
            feedTypeList.ForEach( t => { nativeTypeList.WithType( FeedTypeMap[ t ] ); } );
            return nativeTypeList;
        }

        private static IEnumerable< MwsFeedSubmissionDescriptor > ToFeedSubmissionDescriptors(
            IEnumerable< FeedSubmissionInfo > feedSubmissionInfoList )
        {
            return feedSubmissionInfoList
                .Select( info =>
                    new MwsFeedSubmissionDescriptor {
                        FeedSubmissionId = info.FeedSubmissionId,
                        FeedProcessingStatus = ConvertToFeedSubmissionProcessingStatus( info.FeedProcessingStatus )
                    } )
                .ToList();
        }
    }
}