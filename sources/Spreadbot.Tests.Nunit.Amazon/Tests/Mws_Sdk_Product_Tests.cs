// Spreadbot (c) 2015 Krokodev
// Spreadbot.Tests.Nunit.Amazon
// Mws_Sdk_Product_Tests.cs

using System;
using System.Collections.Generic;
using MarketplaceWebService;
using MarketplaceWebServiceProducts;
using MarketplaceWebServiceProducts.Model;
using NUnit.Framework;
using Spreadbot.Core.Channels.Amazon.Configuration.Settings;
using Spreadbot.Nunit.Amazon.Base;

namespace Spreadbot.Nunit.Amazon.Tests
{
//    [TestFixture]
    public class Mws_Sdk_Product_Tests : Amazon_Tests
    {
        [Test]
        public void Product_information_is_available_by_sku()
        {
            try {
                var service = GetService();

                var request = new GetMatchingProductForIdRequest {
                    SellerId = AmazonSettings.MerchantId,
                    IdType = "SellerSKU",
                    MarketplaceId = "ATVPDKIKX0DER",
                    IdList = new IdListType {
                        Id = new List< string > { "SB_AMZ_002" }
                    }
                };
                var response = service.GetMatchingProductForId( request );
                Console.WriteLine( response.ToXML() );
                Assert_That_Text_Contains( response.ToXML(), "<ns2:Title>Spreadbot Test Item [Attention: Not for Sale!]</ns2:Title>" );
            }
            catch( MarketplaceWebServiceException exception ) {
                if( exception.Message.Contains( "Request is throttled" ) ) {
                    Assert.Inconclusive( "Request is throttled" );
                }
            }
        }

        private static MarketplaceWebServiceProductsClient GetService()
        {
            var mwsConfig = new MarketplaceWebServiceProductsConfig();

            mwsConfig.SetUserAgentHeader( "Speadbot", "1.0", "C#" );

            mwsConfig.ServiceURL = AmazonSettings.ServiceUrl;

            return new MarketplaceWebServiceProductsClient(
                AmazonSettings.AwsAccessKeyId,
                AmazonSettings.AwsSecretAccessKey,
                mwsConfig );
        }
    }
}