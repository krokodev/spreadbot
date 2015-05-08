// Spreadbot (c) 2015 Krokodev
// Spreadbot.Tests.Nunit.Amazon
// Mws_Connector_Product_Tests.cs

using System;
using NUnit.Framework;
using Spreadbot.Core.Channels.Amazon.Services.Mws.Connector;
using Spreadbot.Core.Channels.Amazon.Services.Mws.Results;
using Spreadbot.Nunit.Amazon.Base;
using Spreadbot.Sdk.Common.Operations.Responses;

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
            Console.WriteLine( response );
            response.Check();
            Assert_That_Text_Contains( response.Result.XmlContent, "Spreadbot is a .Net opensource multichannel manager." );
        }

        [Test]
        public void Wrong_sku_involves_readable_error_in_exception()
        {
            const string wrongSku = "wrong sku";
            var response = GetProductInfo( wrongSku );
            Console.WriteLine( response );

            Assert_That_Text_Contains(response.ExceptionInfo, "InvalidParameterValue");
            Assert_That_Text_Contains(response.ExceptionInfo, wrongSku);
            Assert_That_Text_Contains(response.ExceptionInfo, "is an invalid SellerSKU");
        }

        [Test]
        public void Product_AsinId_Title_and_ImageUrl_are_available()
        {
            var response = GetProductInfo( Sku );
            Console.WriteLine( response );
            response.Check();

            Assert.AreEqual( "B00WGHPI3O", response.Result.AsinId );
            Assert.AreEqual( "Spreadbot Test Item [Attention: Not for Sale!]", response.Result.Title );
            Assert.That( response.Result.SmallImageUrl.Contains( "http://" ), "SmallImageUrl" );
        }


        #region Utils

        private const string Sku = "SB_AMZ_002";

        private static Response< MwsGetProductInfoResult > GetProductInfo( string sku )
        {
            var response = MwsConnector.Api.GetProductInfo( sku );
            Ignore_Mws_Throttling( response );
            return response;
        }

        #endregion
    }
}