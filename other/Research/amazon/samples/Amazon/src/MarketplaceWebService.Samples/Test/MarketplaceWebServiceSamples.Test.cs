// MarketplaceWebService (c) 2015 Crocodev
// MarketplaceWebService.Samples
// MarketplaceWebServiceSamples.Test.cs

using System.Collections.Generic;
using System.IO;
using MarketplaceWebService.Model;

namespace MarketplaceWebService.Samples
{
    // Code: Secret
    public partial class MarketplaceWebServiceSamples
    {
        private const string Filename =
            @"p:\Projects\spreadbot\other\Research\amazon\samples\Amazon\src\MarketplaceWebService.Samples\xml\feed.xml";


        const string FeedType = "_POST_PRODUCT_DATA_";

        private static void Test()
        {
            var config = new MarketplaceWebServiceConfig();

            config.SetUserAgentHeader( "", "", "C#" );

            config.ServiceURL = "https://mws.amazonservices.com";


            MarketplaceWebService service = new MarketplaceWebServiceClient(
                Secret.AwsAccessKeyId,
                Secret.AwsSecretAccessKey,
                config );


            var request = new SubmitFeedRequest {
                Merchant = Secret.MerchantId,
                MWSAuthToken = null,//"",
                MarketplaceIdList = new IdList { Id = new List< string >( new string[] { Secret.MarketplaceId } ) },
                FeedContent = File.Open( Filename, FileMode.Open, FileAccess.Read ),
                FeedType = FeedType
            };
            request.ContentMD5 = MarketplaceWebServiceClient.CalculateContentMD5( request.FeedContent );
            request.FeedContent.Position = 0;

            SubmitFeedSample.InvokeSubmitFeed( service, request );
        }
    }
}