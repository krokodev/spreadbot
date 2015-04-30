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
        protected MwsResponse< MwsSubmitFeedResult > _SubmitFeed( MwsFeedHandler feedHandler )
        {
            try {
                var request = new SubmitFeedRequest {
                    Merchant = AmazonSettings.MerchantId,
                    MarketplaceIdList = GetMarketplaceIdList(),
                    FeedContent = GetFeedContentStream( feedHandler ),
                    FeedType = FeedTypeMap[ feedHandler.Type ],
                    ContentMD5 = CalculateContentMd5( feedHandler )
                };

                var response = _mwsClient.SubmitFeed( request );

                return new MwsResponse< MwsSubmitFeedResult > {
                    StatusCode = MwsOperationStatus.SubmitFeedSuccess,
                    Result = new MwsSubmitFeedResult { FeedSubmissionId = TryGetFeedSubmissionId( response ) },
                    Details = response.ToXML()
                };
            }
            catch( Exception exception ) {
                return new MwsResponse< MwsSubmitFeedResult >( exception ) {
                    StatusCode = MwsOperationStatus.SubmitFeedFailure
                };
            }
        }
    }

}