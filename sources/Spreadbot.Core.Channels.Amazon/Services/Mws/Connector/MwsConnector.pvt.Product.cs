// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Amazon
// MwsConnector.pvt.Product.cs

using System;
using System.Collections.Generic;
using MarketplaceWebServiceProducts.Model;
using Spreadbot.Core.Channels.Amazon.Configuration.Settings;
using Spreadbot.Core.Channels.Amazon.Services.Mws.Results;
using Spreadbot.Sdk.Common.Exceptions;
using Spreadbot.Sdk.Common.Operations.Responses;

namespace Spreadbot.Core.Channels.Amazon.Services.Mws.Connector
{
    public partial class MwsConnector
    {
        private Response< MwsGetProductInfoResult > _GetProductInfo( string sku )
        {
            try {
                var request = new GetMatchingProductForIdRequest {
                    SellerId = AmazonSettings.MerchantId,
                    IdType = "SellerSKU",
                    MarketplaceId = "ATVPDKIKX0DER",
                    IdList = new IdListType {
                        Id = new List< string > { sku }
                    }
                };

                var response = _mswProductsClient.GetMatchingProductForId( request );
                if( !response.IsSetGetMatchingProductForIdResult() ) {
                    throw new SpreadbotException( "GetMatchingProductForId return empty result for sku [{0}]", sku );
                }

                return new Response< MwsGetProductInfoResult > {
                    Result = new MwsGetProductInfoResult {
                        XmlContent = response.ToXML(),

                        //Title = response.GetMatchingProductForIdResult[0].Products.Product[0].AttributeSets.
                    }
                };
            }
            catch( Exception exception ) {
                return new Response< MwsGetProductInfoResult >( exception );
            }
        }
    }
}