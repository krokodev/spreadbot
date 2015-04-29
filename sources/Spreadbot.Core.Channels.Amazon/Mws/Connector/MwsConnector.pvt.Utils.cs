// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Amazon
// MwsConnector.pvt.Utils.cs

using System;
using System.Collections.Generic;
using System.IO;
using Krokodev.Common.Extensions;
using MarketplaceWebService;
using MarketplaceWebService.Model;
using Spreadbot.Core.Channels.Amazon.Configuration.Settings;
using Spreadbot.Core.Channels.Amazon.Mws.Feed;
using Spreadbot.Core.Channels.Amazon.Mws.Operations.Response;
using Spreadbot.Core.Channels.Amazon.Mws.Operations.StatusCode;
using Spreadbot.Core.Channels.Amazon.Mws.Results;
using Spreadbot.Sdk.Common.Exceptions;

namespace Spreadbot.Core.Channels.Amazon.Mws.Connector
{
    public partial class MwsConnector
    {
        private MarketplaceWebServiceClient _mwsClient;

        // --------------------------------------------------------[]
        protected MwsResponse< MwsSubmitFeedResult > _SubmitFeed( MwsFeedHandler mwsFeedHandler, string reqId )
        {
            // Code: MwsConnector._SubmitFeed
            // Todo:> Clean up code


            try {
                var fileName = @"{0}Samples\SB_AMZ_002\{1}.Feed.xml".SafeFormat( AmazonSettings.BasePath,
                    mwsFeedHandler.GetName() );

                var request = new SubmitFeedRequest {
                    Merchant = AmazonSettings.MerchantId,
                    MarketplaceIdList =
                        new IdList {
                            Id = new List< string >( new[] { AmazonSettings.MarketplaceId } )
                        },
                    FeedContent = File.Open( fileName, FileMode.Open, FileAccess.Read ),
                    FeedType = "_POST_PRODUCT_DATA_" //mwsFeedHandler.Type.ToString()
                };
                request.ContentMD5 = MarketplaceWebServiceClient.CalculateContentMD5( request.FeedContent );
                request.FeedContent.Position = 0;
                var mwsResponse = _mwsClient.SubmitFeed( request );

                return new MwsResponse< MwsSubmitFeedResult > {
                    StatusCode = MwsOperationStatus.SubmitFeedSuccess,
                    Result = new MwsSubmitFeedResult { MwsRequestId = TryGetFeedSubmissionId( mwsResponse ) },
                    Details = mwsResponse.ToXML()
                };
            }
            catch( Exception exception ) {
                return new MwsResponse< MwsSubmitFeedResult >( exception ) {
                    StatusCode = MwsOperationStatus.SubmitFeedFailure
                };
            }
        }


        private static string TryGetFeedSubmissionId( SubmitFeedResponse mwsResponse )
        {
            try {
                return mwsResponse.SubmitFeedResult.FeedSubmissionInfo.FeedSubmissionId;
            }
            catch {
                throw new SpreadbotException( "SubmitFeedResponse has no FeedSubmissionId" );
            }
        }

        private void InitServiceClient()
        {
            var mwsConfig = new MarketplaceWebServiceConfig();
            mwsConfig.SetUserAgentHeader( "Speadbot", "1.0", "C#" );
            mwsConfig.ServiceURL = AmazonSettings.ServiceUrl;

            _mwsClient = new MarketplaceWebServiceClient(
                AmazonSettings.AwsAccessKeyId,
                AmazonSettings.AwsSecretAccessKey,
                mwsConfig );
        }
    }
}