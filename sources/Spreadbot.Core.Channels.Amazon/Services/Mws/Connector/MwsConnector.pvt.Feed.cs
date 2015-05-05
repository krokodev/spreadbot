// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Amazon
// MwsConnector.pvt.Feed.cs

using System;
using System.Collections.Generic;
using MarketplaceWebService.Model;
using Spreadbot.Core.Channels.Amazon.Configuration.Settings;
using Spreadbot.Core.Channels.Amazon.Services.Mws.Feed;
using Spreadbot.Core.Channels.Amazon.Services.Mws.FeedSubmission;
using Spreadbot.Core.Channels.Amazon.Services.Mws.Results;
using Spreadbot.Sdk.Common.Operations.Responses;

namespace Spreadbot.Core.Channels.Amazon.Services.Mws.Connector
{
    public partial class MwsConnector
    {
        protected Response< MwsSubmitFeedResult > _SubmitFeed( MwsFeedDescriptor feedDescriptor )
        {
            try {
                var request = new SubmitFeedRequest {
                    Merchant = AmazonSettings.MerchantId,
                    MarketplaceIdList = GetMarketplaceIdList(),
                    FeedContent = GetFeedContentStream( feedDescriptor ),
                    FeedType = FeedTypeMap[ feedDescriptor.Type ],
                    ContentMD5 = CalculateContentMd5( feedDescriptor )
                };

                var response = _mwsClient.SubmitFeed( request );

                return new Response< MwsSubmitFeedResult > {
                    Result = new MwsSubmitFeedResult { FeedSubmissionId = TryGetFeedSubmissionId( response ) },
                    Details = response.ToXML()
                };
            }
            catch( Exception exception ) {
                return new Response< MwsSubmitFeedResult >( exception );
            }
        }

        private Response< MwsGetFeedSubmissionListResult > _GetFeedSubmissionList( MwsSubmittedFeedsFilter filter )
        {
            try {
                var descriptors = new List< MwsFeedSubmissionDescriptor >();

                var response = RunGetFeedSubmissionList( filter );
                descriptors.AddRange( TryGetFeedSubmissionDescriptors( response ) );

                var nextToken = response.GetFeedSubmissionListResult.NextToken;
                descriptors.AddRange( GetNextFeedSubmissionDescriptors( nextToken ) );

                return new Response< MwsGetFeedSubmissionListResult > {
                    Result = new MwsGetFeedSubmissionListResult {
                        FeedSubmissionDescriptors = descriptors
                    },
                    Details = response.ToXML()
                };
            }
            catch( Exception exception ) {
                return new Response< MwsGetFeedSubmissionListResult >( exception );
            }
        }

        private IEnumerable< MwsFeedSubmissionDescriptor > GetNextFeedSubmissionDescriptors( string nextToken )
        {
            var descriptors = new List< MwsFeedSubmissionDescriptor >();
            while( !string.IsNullOrEmpty( nextToken ) ) {
                var nextResponse = RunGetFeedSubmissionListByNextToken( nextToken );
                descriptors.AddRange( TryGetFeedSubmissionDescriptors( nextResponse ) );
                nextToken = GetNextToken( nextResponse );
            }
            return descriptors;
        }

        private GetFeedSubmissionListByNextTokenResponse RunGetFeedSubmissionListByNextToken( string nextToken )
        {
            var request = new GetFeedSubmissionListByNextTokenRequest {
                Merchant = AmazonSettings.MerchantId,
                NextToken = nextToken
            };
            return _mwsClient.GetFeedSubmissionListByNextToken( request );
        }

        private GetFeedSubmissionListResponse RunGetFeedSubmissionList( MwsSubmittedFeedsFilter filter )
        {
            var request = new GetFeedSubmissionListRequest {
                Merchant = AmazonSettings.MerchantId,
                MaxCount = GetFeedSubmissionsChunkSize,
                SubmittedFromDate = filter.FromDate,
                SubmittedToDate = filter.ToDate,
                FeedSubmissionIdList = ConvertToNativeIdList( filter.IdList ),
                FeedProcessingStatusList = ConvertToNativeStatusList( filter.ProcessingStatusList ),
                FeedTypeList = ConvertToNativeTypeList( filter.FeedTypeList ),
            };
            return _mwsClient.GetFeedSubmissionList( request );
        }

        private Response< MwsGetFeedSubmissionCountResult > _GetFeedSubmissionCount( MwsSubmittedFeedsFilter filter )
        {
            try {
                var request = new GetFeedSubmissionCountRequest {
                    Merchant = AmazonSettings.MerchantId,
                    SubmittedFromDate = filter.FromDate,
                    SubmittedToDate = filter.ToDate,
                    FeedProcessingStatusList = ConvertToNativeStatusList( filter.ProcessingStatusList ),
                    FeedTypeList = ConvertToNativeTypeList( filter.FeedTypeList ),
                };

                var response = _mwsClient.GetFeedSubmissionCount( request );

                return new Response< MwsGetFeedSubmissionCountResult > {
                    Result = new MwsGetFeedSubmissionCountResult {
                        FeedSubmissionCount = TryGetFeedSubmissionCount( response )
                    },
                    Details = response.ToXML()
                };
            }
            catch( Exception exception ) {
                return new Response< MwsGetFeedSubmissionCountResult >( exception );
            }
        }

        private Response< MwsGetFeedSubmissionProcessingStatusResult > _GetFeedSubmissionProcessingStatus(
            string feedSubmissionId )
        {
            try {
                var listResponse = _GetFeedSubmissionList( MwsSubmittedFeedsFilter.WithId( feedSubmissionId ) );
                var descriptor = TryGetFeedSubmissionDescriptor( listResponse, feedSubmissionId );

                return new Response< MwsGetFeedSubmissionProcessingStatusResult > {
                    Result = new MwsGetFeedSubmissionProcessingStatusResult {
                        FeedSubmissionProcessingStatus = descriptor != null
                            ? descriptor.FeedProcessingStatus
                            : MwsFeedSubmissionProcessingStatus.Unknown
                    },
                    InnerResponses = new List< IAbstractResponse > { listResponse }
                };
            }
            catch( Exception exception ) {
                return new Response< MwsGetFeedSubmissionProcessingStatusResult >( exception );
            }
        }
    }
}