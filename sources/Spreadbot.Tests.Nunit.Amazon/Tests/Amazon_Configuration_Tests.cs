// Spreadbot (c) 2015 Krokodev
// Spreadbot.Tests.Nunit.Amazon
// Amazon_Configuration_Tests.cs

using NUnit.Framework;
using Spreadbot.Core.Channels.Amazon.Configuration.Sections;
using Spreadbot.Nunit.Amazon.Base;

namespace Spreadbot.Nunit.Amazon.Tests
{
    [TestFixture]
    public class Amazon_Configuration_Tests : Amazom_Tests
    {
        [Test]
        public void Read_Amazon_Config()
        {
            var configuration = AmazonPublicConfig.Instance;
            Assert.AreEqual( "https://mws.amazonservices.com", configuration.MwsConnection.ServiceUrl );
            Assert.AreEqual( "ATVPDKIKX0DER", configuration.MwsConnection.MarketplaceId );
        }

        [Test]
        public void Read_Amazon_Security_Config()
        {
            var configuration = AmazonSecretConfig.Instance;
            Assert.AreEqual( "A39AOPPISH8HQ0", configuration.MwsSecretData.MerchantId, 
                @"Please edit records in [App_data\Configs\Amazon.Secret.config]" );
        }
    }
}