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
using Spreadbot.Core.Channels.Amazon.Services.Mws.FeedSubmission;
using Spreadbot.Sdk.Common.Exceptions;

namespace Spreadbot.Core.Channels.Amazon.Services.Mws.Connector
{
    public partial class MwsConnector
    {
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
                throw new SpreadbotException( "Can't TryGetFeedSubmissionId [{0}]", exception.Message );
            }
        }

        private static IEnumerable< MwsFeedSubmissionDescriptor > TryGetFeedSubmissionDescriptors(
            GetFeedSubmissionListResponse response )
        {
            try {
                return response.GetFeedSubmissionListResult.FeedSubmissionInfo
                    .Select( info => new MwsFeedSubmissionDescriptor {
                        FeedSubmissionId = info.FeedSubmissionId
                    } )
                    .ToList();
            }
            catch( Exception exception ) {
                throw new SpreadbotException( "Can't TryGetFeedSubmissionDescriptors [{0}]", exception.Message );
            }
        }

        private static IEnumerable< MwsFeedSubmissionDescriptor > TryGetFeedSubmissionDescriptors( GetFeedSubmissionListByNextTokenResponse response )
        {
            try {
                return response.GetFeedSubmissionListByNextTokenResult.FeedSubmissionInfo
                    .Select( info => new MwsFeedSubmissionDescriptor {
                        FeedSubmissionId = info.FeedSubmissionId
                    } )
                    .ToList();
            }
            catch( Exception exception ) {
                throw new SpreadbotException( "Can't TryGetFeedSubmissionDescriptors [{0}]", exception.Message );
            }
        }

        private static int TryGetFeedSubmissionCount( GetFeedSubmissionCountResponse response )
        {
            try {
                return ( int ) response.GetFeedSubmissionCountResult.Count;
            }
            catch( Exception exception ) {
                throw new SpreadbotException( "Can't TryGetFeedSubmissionCount [{0}]", exception.Message );
            }
        }
    }
}