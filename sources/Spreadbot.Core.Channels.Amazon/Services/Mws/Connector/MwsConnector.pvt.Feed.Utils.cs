// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Amazon
// MwsConnector.pvt.Feed.Utils.cs

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using MarketplaceWebService;
using MarketplaceWebService.Model;
using Spreadbot.Core.Channels.Amazon.Configuration.Settings;
using Spreadbot.Core.Channels.Amazon.Services.Mws.Feed;
using Spreadbot.Sdk.Common.Exceptions;

namespace Spreadbot.Core.Channels.Amazon.Services.Mws.Connector
{
    public partial class MwsConnector
    {
        private const string FeedContentEncoding = "ISO-8859-1";

        private static readonly Dictionary< MwsFeedType, string > FeedTypeMap =
            new Dictionary< MwsFeedType, string > {
                { MwsFeedType.None, "_NONE_" },
                { MwsFeedType.Image, "_POST_PRODUCT_IMAGE_DATA_" },
                { MwsFeedType.Product, "_POST_PRODUCT_DATA_" },
                { MwsFeedType.Inventory, "_POST_INVENTORY_AVAILABILITY_DATA_" },
                { MwsFeedType.Price, "_POST_PRODUCT_PRICING_DATA_" }
            };

        private static string CalculateContentMd5( MwsFeedDescriptor feedDescriptor )
        {
            var stream = GetFeedContentStream( feedDescriptor );
            return MarketplaceWebServiceClient.CalculateContentMD5( stream );
        }

        private static IdList GetMarketplaceIdList()
        {
            return new IdList {
                Id = new List< string >( new[] { AmazonSettings.MarketplaceId } )
            };
        }

        private static MemoryStream GetFeedContentStream( MwsFeedDescriptor feedDescriptor )
        {
            return new MemoryStream( Encoding.GetEncoding( FeedContentEncoding ).GetBytes( feedDescriptor.Content ) );
        }

        private static string TryGetFeedSubmissionId( SubmitFeedResponse response )
        {
            try {
                return response.SubmitFeedResult.FeedSubmissionInfo.FeedSubmissionId;
            }
            catch( Exception exception ) {
                throw new SpreadbotException( "Can't get FeedSubmissionId [{0}]", exception.Message );
            }
        }

        private static List< string > TryGetFeedSubmissionIds( GetFeedSubmissionListResponse response )
        {
            try {
                return response.GetFeedSubmissionListResult.FeedSubmissionInfo
                    .Select( info => info.FeedSubmissionId )
                    .ToList();
            }
            catch( Exception exception ) {
                throw new SpreadbotException( "Can't get FeedSubmissionList [{0}]", exception.Message );
            }

            throw new NotImplementedException();
        }
    }
}