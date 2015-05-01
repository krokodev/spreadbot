// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Amazon
// MwsConnector.pvt.Feed.cs

using System;
using MarketplaceWebService.Model;
using Spreadbot.Core.Channels.Amazon.Configuration.Settings;
using Spreadbot.Core.Channels.Amazon.Services.Mws.Feed;
using Spreadbot.Core.Channels.Amazon.Services.Mws.Operations.Responses;
using Spreadbot.Core.Channels.Amazon.Services.Mws.Operations.Results;

namespace Spreadbot.Core.Channels.Amazon.Services.Mws.Connector
{
    public partial class MwsConnector
    {
        protected MwsResponse< MwsSubmitFeedResult > _SubmitFeed( MwsFeedDescriptor feedDescriptor )
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

                return new MwsResponse< MwsSubmitFeedResult > {
                    Result = new MwsSubmitFeedResult { FeedSubmissionId = TryGetFeedSubmissionId( response ) },
                    Details = response.ToXML()
                };
            }
            catch( Exception exception ) {
                return new MwsResponse< MwsSubmitFeedResult >( exception );
            }
        }

        private MwsResponse< MwsGetFeedSubmissionsResult > _GetFeedSubmissions()
        {
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

                return new MwsResponse< MwsGetFeedSubmissionsResult > {
                    Result = new MwsGetFeedSubmissionsResult {
                        FeedSubmissionDescriptors = TryGetFeedSubmissionDescriptors( response )
                    },
                    Details = response.ToXML()
                };
            }
            catch( Exception exception ) {
                return new MwsResponse< MwsGetFeedSubmissionsResult >( exception );
            }
        }

        private MwsResponse< MwsGetFeedSubmissionCountResult > _GetFeedSubmissionCount()
        {
            // Todo:> Use arg Filters
            try {
                var request = new GetFeedSubmissionCountRequest {
                    Merchant = AmazonSettings.MerchantId,
                    SubmittedFromDate = DateTime.Today,
                    FeedProcessingStatusList = new StatusList { Status = { "_DONE_" } }
                };

                var response = _mwsClient.GetFeedSubmissionCount( request );

                return new MwsResponse< MwsGetFeedSubmissionCountResult > {
                    Result = new MwsGetFeedSubmissionCountResult {
                        FeedSubmissionCount = TryGetFeedSubmissionCount( response )
                    },
                    Details = response.ToXML()
                };
            }
            catch( Exception exception ) {
                return new MwsResponse< MwsGetFeedSubmissionCountResult >( exception );
            }
        }
    }
}