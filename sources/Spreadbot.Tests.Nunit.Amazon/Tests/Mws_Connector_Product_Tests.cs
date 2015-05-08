// Spreadbot (c) 2015 Krokodev
// Spreadbot.Tests.Nunit.Amazon
// Mws_Connector_Product_Tests.cs

using System;
using NUnit.Framework;
using Spreadbot.Core.Channels.Amazon.Services.Mws.Connector;
using Spreadbot.Core.Channels.Amazon.Services.Mws.Results;
using Spreadbot.Nunit.Amazon.Base;
using Spreadbot.Sdk.Common.Operations.Responses;

// Code: Mws_Connector_Product_Tests

namespace Spreadbot.Nunit.Amazon.Tests
{
    [TestFixture]
    public class Mws_Connector_Product_Tests : Amazon_Tests
    {
        [SetUp]
        public static void Init() {}

        [Test]
        public void Submitted_product_Asin_xml_content_is_available()
        {
            var response = GetProductInfo( Sku );
            response.Check();
            Assert_That_Text_Contains( response.Result.XmlContent, "Spreadbot is a .Net opensource multichannel manager." );
        }

        [Test]
        public void Wrong_sku_involves_readable_error()
        {
            const string wrongSku = "wrong sku";
            var response = GetProductInfo( wrongSku );
            Assert_That_Text_Contains(response.Result.XmlContent, "InvalidParameterValue");
            Assert_That_Text_Contains(response.Result.XmlContent, wrongSku);
            Assert_That_Text_Contains(response.Result.XmlContent, "is an invalid SellerSKU");
        }

        [Test]
        public void Product_Asin_Title_Image_are_available()
        {
            var response = GetProductInfo( Sku );
            response.Check();

            Assert.AreEqual( "B00WGHPI3O", response.Result.AsinId );
            Assert.AreEqual( "Spreadbot Test Item [Attention: Not for Sale!]", response.Result.Title );
            Assert.IsNotNullOrEmpty( response.Result.ImageUrl, "ImageUrl" );
        }


        #region Utils

        private const string Sku = "SB_AMZ_002";

        private static Response< MwsGetProductInfoResult > GetProductInfo( string sku )
        {
            var response = MwsConnector.Api.GetProductInfo( sku );
            Ignore_Mws_Throttling( response );
            Console.WriteLine( response );
            return response;
        }

        #endregion
    }
}