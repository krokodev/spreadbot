// Spreadbot (c) 2015 Krokodev
// Spreadbot.Tests.Nunit.Amazon
// Amazon_Configuration_Tests.cs

using NUnit.Framework;
using Spreadbot.Core.Channels.Amazon.Configuration.Settings;
using Spreadbot.Nunit.Amazon.Base;

namespace Spreadbot.Nunit.Amazon.Tests
{
    [TestFixture]
    public class Amazon_Configuration_Tests : Amazom_Tests
    {
        [Test]
        public void Read_Amazon_Config()
        {
            Assert.AreEqual( "https://mws.amazonservices.com", AmazonSettings.ServiceUrl );
            Assert.AreEqual( "ATVPDKIKX0DER", AmazonSettings.MarketplaceId );
        }

        [Test]
        public void Read_Amazon_Secret_Config()
        {
            Assert.AreEqual( "A39AOPPISH8HQ0",
                AmazonSettings.MerchantId,
                @"Please edit records in [App_data\Configs\Amazon.Secret.config]" );
        }
    }
}