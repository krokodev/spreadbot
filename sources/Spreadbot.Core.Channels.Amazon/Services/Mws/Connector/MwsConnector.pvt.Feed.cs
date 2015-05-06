// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Amazon
// MwsConnector.pvt.Feed.cs

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
                    FeedContent = GetStreamToReadContent( feedDescriptor.Content ),
                    FeedType = FeedTypeMap[ feedDescriptor.Type ],
                    ContentMD5 = CalculateMd5( feedDescriptor.Content )
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
                var descriptor = GetFeedSubmissionDescriptor( listResponse, feedSubmissionId );

                return new Response< MwsGetFeedSubmissionProcessingStatusResult > {
                    Result = new MwsGetFeedSubmissionProcessingStatusResult {
                        FeedSubmissionProcessingStatus = GetFeedSubmissionProcessingStatus( descriptor )
                    },
                    InnerResponses = new List< IAbstractResponse > { listResponse }
                };
            }
            catch( Exception exception ) {
                return new Response< MwsGetFeedSubmissionProcessingStatusResult >( exception );
            }
        }

        private Response< MwsGetFeedSubmissionStatusResult > _GetFeedSubmissionStatus( string feedSubmissionId )
        {
            try {
                var stream = new MemoryStream();

                var request = new GetFeedSubmissionResultRequest {
                    Merchant = AmazonSettings.MerchantId,
                    FeedSubmissionId = feedSubmissionId,
                    FeedSubmissionResult = stream
                };

                var response = _mwsClient.GetFeedSubmissionResult( request );
                var content = Encoding.GetEncoding( FeedContentEncoding ).GetString( stream.ToArray() );
                CheckContentMD5IsEqual( content, response.GetFeedSubmissionResultResult.ContentMD5 );

                return new Response< MwsGetFeedSubmissionStatusResult > {
                    Result = GetMwsGetFeedSubmissionStatusResult( content ),
                    Details = response.ToXML()
                };
            }
            catch( Exception exception ) {
                return new Response< MwsGetFeedSubmissionStatusResult >( exception );
            }
        }

        private Response< MwsGetFeedSubmissionOverallStatusResult > _GetFeedSubmissionAverallStatus(
            string feedSubmissionId )
        {
            try {
                var responseProcessing = _GetFeedSubmissionProcessingStatus( feedSubmissionId ).Check();
                var overallStatus = MwsFeedSubmissionOverallStatus.Unknown;
                var innerResponses = new List< IAbstractResponse > { responseProcessing };

                switch( responseProcessing.Result.FeedSubmissionProcessingStatus ) {
                    case MwsFeedSubmissionProcessingStatus.Unknown :
                        overallStatus = MwsFeedSubmissionOverallStatus.Unknown;
                        break;
                    case MwsFeedSubmissionProcessingStatus.Cancelled :
                        overallStatus = MwsFeedSubmissionOverallStatus.Failure;
                        break;
                    case MwsFeedSubmissionProcessingStatus.InProgress :
                        overallStatus = MwsFeedSubmissionOverallStatus.InProgress;
                        break;
                    case MwsFeedSubmissionProcessingStatus.Done :
                        IAbstractResponse responseComplete;
                        overallStatus = GetOverallStatusBasedOnComplete(feedSubmissionId, out responseComplete);
                        innerResponses.Add(responseComplete);
                        break;
                }
                return new Response< MwsGetFeedSubmissionOverallStatusResult > {
                    Result = new MwsGetFeedSubmissionOverallStatusResult {
                        FeedSubmissionOverallStatus = overallStatus
                    },
                    InnerResponses = innerResponses
                };
            }
            catch( Exception exception ) {
                return new Response< MwsGetFeedSubmissionOverallStatusResult >( exception );
            }
        }

        private MwsFeedSubmissionOverallStatus GetOverallStatusBasedOnComplete(
            string feedSubmissionId,
            out IAbstractResponse response )
        {
            var responseComplete = _GetFeedSubmissionStatus( feedSubmissionId ).Check();
            response = responseComplete;
            switch( responseComplete.Result.FeedSubmissionStatus ) {
                case MwsFeedSubmissionStatus.Unknown :
                    return MwsFeedSubmissionOverallStatus.Unknown;
                case MwsFeedSubmissionStatus.Failure:
                    return MwsFeedSubmissionOverallStatus.Failure;
                case MwsFeedSubmissionStatus.Success:
                    return MwsFeedSubmissionOverallStatus.Success;
            }
            return MwsFeedSubmissionOverallStatus.Unknown;
        }
    }
}