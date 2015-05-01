// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Amazon
// MwsConnector.pvt.Feed.cs

using System;
using System.Collections.Generic;
using System.Linq;
using MarketplaceWebService.Model;
using Spreadbot.Core.Channels.Amazon.Configuration.Settings;
using Spreadbot.Core.Channels.Amazon.Services.Mws.Feed;
using Spreadbot.Core.Channels.Amazon.Services.Mws.Operations.Responses;
using Spreadbot.Core.Channels.Amazon.Services.Mws.Operations.Results;
using Spreadbot.Core.Channels.Amazon.Services.Mws.Operations.Statuses;
using Spreadbot.Sdk.Common.Exceptions;

namespace Spreadbot.Core.Channels.Amazon.Services.Mws.Connector
{
    public partial class MwsConnector
    {
        protected MwsSubmitFeedResponse _SubmitFeed( MwsFeedDescriptor feedDescriptor )
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

                return new MwsSubmitFeedResponse {
                    StatusCode = MwsOperationStatus.SubmitFeedSuccess,
                    Result = new MwsSubmitFeedResult { FeedSubmissionId = TryGetFeedSubmissionId( response ) },
                    Details = response.ToXML()
                };
            }
            catch( Exception exception ) {
                return new MwsSubmitFeedResponse( exception ) {
                    StatusCode = MwsOperationStatus.SubmitFeedFailure
                };
            }
        }

        private MwsGetFeedSubmissionListResponse _GetFeedSubmissionList()
        {
            // Code: GetFeedSubmissionList
            // Todo:> Use arg Filters, Next Tokens
            try {
                var request = new GetFeedSubmissionListRequest {
                    Merchant = AmazonSettings.MerchantId,
                    MaxCount = 100,
                    SubmittedFromDate = DateTime.Today,
                    FeedSubmissionIdList = null,
                    FeedProcessingStatusList = new StatusList { Status = { "_DONE_" } }
                };

                var response = _mwsClient.GetFeedSubmissionList( request );

                return new MwsGetFeedSubmissionListResponse {
                    StatusCode = MwsOperationStatus.GetFeedSubmissionListSuccess,
                    Result = new MwsGetFeedSubmissionListResult {
                        FeedSubmissionDescriptors = TryGetFeedSubmissionDescriptors( response )
                    },
                    Details = response.ToXML()
                };
            }
            catch( Exception exception ) {
                return new MwsGetFeedSubmissionListResponse( exception ) {
                    StatusCode = MwsOperationStatus.GetFeedSubmissionListFailure
                };
            }
        }
    }
}