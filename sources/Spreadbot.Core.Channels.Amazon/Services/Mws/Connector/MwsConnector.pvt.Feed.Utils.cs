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
using Spreadbot.Sdk.Common.Krokodev.Common;
using Spreadbot.Sdk.Common.Operations.Responses;

namespace Spreadbot.Core.Channels.Amazon.Services.Mws.Connector
{
    public partial class MwsConnector
    {
        private static string CalculateMd5( string content )
        {
            var stream = GetStreamToReadContent( content );
            return MarketplaceWebServiceClient.CalculateContentMD5( stream );
        }

        private static IdList GetMarketplaceIdList()
        {
            return new IdList {
                Id = new List< string >( new[] { AmazonSettings.MarketplaceId } )
            };
        }

        private static MemoryStream GetStreamToReadContent( string content )
        {
            return new MemoryStream( Encoding.GetEncoding( FeedContentEncoding ).GetBytes( content ) );
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

        private static MwsFeedSubmissionStatus GetFeedSubmissionStatusFromContent( string content )
        {
            try {
                return content.GetXmlValue( "/AmazonEnvelope/Message/ProcessingReport/StatusCode" ) == "Complete"
                    ? MwsFeedSubmissionStatus.Success
                    : MwsFeedSubmissionStatus.Unknown;
            }
            catch {
                return MwsFeedSubmissionStatus.Unknown;
            }
        }

        private static string GetNextToken( GetFeedSubmissionListByNextTokenResponse nextResponse )
        {
            return nextResponse.IsSetGetFeedSubmissionListByNextTokenResult()
                && nextResponse.GetFeedSubmissionListByNextTokenResult.HasNext
                ? nextResponse.GetFeedSubmissionListByNextTokenResult.NextToken
                : null;
        }

        private static MwsFeedSubmissionDescriptor GetFeedSubmissionDescriptor(
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

        private static MwsFeedSubmissionProcessingStatus GetFeedSubmissionProcessingStatus(
            MwsFeedSubmissionDescriptor descriptor )
        {
            return descriptor != null
                ? descriptor.FeedProcessingStatus
                : MwsFeedSubmissionProcessingStatus.Unknown;
        }

        private static MwsGetFeedSubmissionStatusResult GetMwsGetFeedSubmissionStatusResult( string content )
        {
            var root = "/AmazonEnvelope/Message/ProcessingReport/ProcessingSummary/";
            var result = new MwsGetFeedSubmissionStatusResult {
                FeedSubmissionStatus = GetFeedSubmissionStatusFromContent( content ),
                TotalProcessedCount = content.GetXmlIntValue( root + "MessagesProcessed" ),
                SuccessfulCount = content.GetXmlIntValue( root + "MessagesSuccessful" ),
                WithErrorCount = content.GetXmlIntValue( root + "MessagesWithError" ),
                WithWarningCount = content.GetXmlIntValue( root + "MessagesWithWarning" ),
                Content = content
            };

            if( result.TotalProcessedCount == null || result.SuccessfulCount == null || result.WithErrorCount == null
                || result.WithWarningCount == null ) {
                result.FeedSubmissionStatus = MwsFeedSubmissionStatus.Unknown;
                return result;
            }

            if( ( int ) result.WithErrorCount > 0 ) {
                result.FeedSubmissionStatus = MwsFeedSubmissionStatus.Failure;
                return result;
            }

            return result;
        }

        private void CheckContentMD5IsEqual( string content, string contentMd5 )
        {
            if( CalculateMd5( content ) != contentMd5 ) {
                
            }
        }
    }
}