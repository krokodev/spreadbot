// Spreadbot (c) 2015 Krokodev
// Spreadbot.Core.Channels.Amazon
// MwsConnector.pvt.Product.cs

using System;
using System.Collections.Generic;
using System.Xml;
using MarketplaceWebServiceProducts.Model;
using Spreadbot.Core.Channels.Amazon.Configuration.Settings;
using Spreadbot.Core.Channels.Amazon.Services.Mws.Results;
using Spreadbot.Sdk.Common.Exceptions;
using Spreadbot.Sdk.Common.Krokodev.Common;
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
                        AsinId = GeProductAsinId( response ),
                        Title =  GetProductTitle( response )
                    }
                };
            }
            catch( Exception exception ) {
                return new Response< MwsGetProductInfoResult >( exception );
            }
        }

        private static string GetProductTitle( GetMatchingProductForIdResponse response )
        {

            var xmlElement = ( XmlElement ) response.GetMatchingProductForIdResult[ 0 ].Products.Product[ 0 ].AttributeSets.Any[ 0 ];

            var xmlContent = ProductsUtil.FormatXml(xmlElement);

            var xmlDoc = new XmlDocument {
                InnerXml = xmlContent,
            };

            var nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);
            nsmgr.AddNamespace("ns2", "http://mws.amazonservices.com/schema/Products/2011-10-01/default.xsd");

            Console.WriteLine("================================");
            Console.WriteLine(xmlContent);
            Console.WriteLine("================================");

            var itemIdNode = xmlDoc.SelectSingleNode( "/ns2:ItemAttributes/ns2:Title",  nsmgr);

            if( itemIdNode != null ) {
                return itemIdNode.InnerText;
            }
            return "itemIdNode == null";

            /*
                .Replace( "<ns2:", "<")
                .Replace( "</ns2:", "</")
                .GetXmlValue( "/AttributeSetList" );
 * 
*/
        }

        private static string GeProductAsinId( GetMatchingProductForIdResponse response )
        {
            return response.GetMatchingProductForIdResult[ 0 ].Products.Product[ 0 ].Identifiers.MarketplaceASIN.ASIN;
        }
    }
}