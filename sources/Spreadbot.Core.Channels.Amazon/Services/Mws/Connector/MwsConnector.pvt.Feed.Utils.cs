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
using Spreadbot.Core.Channels.Amazon.Services.Mws.Results;
using Spreadbot.Sdk.Common.Exceptions;
using Spreadbot.Sdk.Common.Operations.Responses;

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
                throw new SpreadbotException( "TryGetFeedSubmissionId [{0}]", exception.Message );
            }
        }

        private static IEnumerable< MwsFeedSubmissionDescriptor > TryGetFeedSubmissionDescriptors(
            GetFeedSubmissionListResponse response )
        {
            try {
                return ToFeedSubmissionDescriptors( response.GetFeedSubmissionListResult.FeedSubmissionInfo );
            }
            catch( Exception exception ) {
                throw new SpreadbotException( "TryGetFeedSubmissionDescriptors [{0}]", exception.Message );
            }
        }

        private static IEnumerable< MwsFeedSubmissionDescriptor > TryGetFeedSubmissionDescriptors(
            GetFeedSubmissionListByNextTokenResponse response )
        {
            try {
                return ToFeedSubmissionDescriptors( response.GetFeedSubmissionListByNextTokenResult.FeedSubmissionInfo );
            }
            catch( Exception exception ) {
                throw new SpreadbotException( "TryGetFeedSubmissionDescriptors [{0}]", exception.Message );
            }
        }

        private static int TryGetFeedSubmissionCount( GetFeedSubmissionCountResponse response )
        {
            try {
                return ( int ) response.GetFeedSubmissionCountResult.Count;
            }
            catch( Exception exception ) {
                throw new SpreadbotException( "TryGetFeedSubmissionCount [{0}]", exception.Message );
            }
        }

        private static string GetNextToken( GetFeedSubmissionListByNextTokenResponse nextResponse )
        {
            return nextResponse.IsSetGetFeedSubmissionListByNextTokenResult()
                && nextResponse.GetFeedSubmissionListByNextTokenResult.HasNext
                ? nextResponse.GetFeedSubmissionListByNextTokenResult.NextToken
                : null;
        }

        private static MwsFeedSubmissionDescriptor TryGetFeedSubmissionDescriptor(
            Response< MwsGetFeedSubmissionListResult > listResponse,
            string feedSubmissionId )
        {
            if( listResponse.IsSuccessful ) {
                return listResponse.Result.FeedSubmissionDescriptors.FirstOrDefault(
                    d => d.FeedSubmissionId == feedSubmissionId
                    );
            }
            return null;
        }
    }
}