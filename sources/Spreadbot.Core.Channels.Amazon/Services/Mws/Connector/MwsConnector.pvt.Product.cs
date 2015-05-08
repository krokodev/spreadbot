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
using Spreadbot.Sdk.Common.Operations.Responses;

namespace Spreadbot.Core.Channels.Amazon.Services.Mws.Connector
{
    public partial class MwsConnector
    {
        #region Product API Implementation

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

                CheckForErrors( response );

                return new Response< MwsGetProductInfoResult > {
                    Result = new MwsGetProductInfoResult {
                        XmlContent = response.ToXML(),
                        AsinId = GeProductAsinId( response ),
                        Title = GetProductTitle( response ),
                        SmallImageUrl = GetProductSmallImageUrl( response )
                    }
                };
            }
            catch( Exception exception ) {
                return new Response< MwsGetProductInfoResult >( exception );
            }
        }

        #endregion



        #region Private Utils

        private static void CheckForErrors( GetMatchingProductForIdResponse response )
        {
            if( response.GetMatchingProductForIdResult[ 0 ].IsSetError() ) {
                throw new SpreadbotException( response.ToXML() );
            }
        }

        private static string GetProductTitle( GetMatchingProductForIdResponse response )
        {
            return GetProductAttribute( response, "/ns2:ItemAttributes/ns2:Title" );
        }

        private static string GetProductSmallImageUrl( GetMatchingProductForIdResponse response )
        {
            return GetProductAttribute( response, "/ns2:ItemAttributes/ns2:SmallImage/ns2:URL" );
        }

        private static string GetProductAttribute( GetMatchingProductForIdResponse response, string path )
        {
            var xmlElement = ( XmlElement ) response.GetMatchingProductForIdResult[ 0 ].Products.Product[ 0 ].AttributeSets.Any[ 0 ];
            var xmlContent = ProductsUtil.FormatXml( xmlElement );
            var xmlDoc = new XmlDocument {
                InnerXml = xmlContent,
            };
            var namespaceManager = new XmlNamespaceManager( xmlDoc.NameTable );
            namespaceManager.AddNamespace( "ns2", "http://mws.amazonservices.com/schema/Products/2011-10-01/default.xsd" );

            var itemIdNode = xmlDoc.SelectSingleNode( path, namespaceManager );

            return itemIdNode != null ? itemIdNode.InnerText : null;
        }

        private static string GeProductAsinId( GetMatchingProductForIdResponse response )
        {
            return response.GetMatchingProductForIdResult[ 0 ].Products.Product[ 0 ].Identifiers.MarketplaceASIN.ASIN;
        }

        #endregion
    }
}