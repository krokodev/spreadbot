// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Amazon
// MwsConnector.pvt.Feed.cs

using System;
using MarketplaceWebService.Model;
using Spreadbot.Core.Channels.Amazon.Configuration.Settings;
using Spreadbot.Core.Channels.Amazon.Services.Mws.Feed;
using Spreadbot.Core.Channels.Amazon.Services.Mws.Operations.Response;
using Spreadbot.Core.Channels.Amazon.Services.Mws.Operations.Results;
using Spreadbot.Core.Channels.Amazon.Services.Mws.Operations.StatusCode;

// Code: MwsConnector.Feed

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
            /*
            try {
                var response = GetFeedSubmissionDoneList();
                if( response.GetFeedSubmissionListResult.HasNext ) {
                    Assert.Inconclusive( "Too many completed feed submissions, need to use Next Token" );
                }

                var recentFeedSubmissionIds =
                    response.GetFeedSubmissionListResult.FeedSubmissionInfo.Where( info => info.IsSetFeedSubmissionId() )
                        .Reverse()
                        .ToList();
                var recentNIds =
                    recentFeedSubmissionIds.Skip( Math.Max( 0, recentFeedSubmissionIds.Count() - RecentFeedsNumber ) )
                        .ToList();
                if( !recentNIds.Any() ) {
                    Assert.Inconclusive( "No completed feed submissions" );
                }
            }
*/
            throw new NotImplementedException();
        }
    }
}