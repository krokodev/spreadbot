// Spreadbot (c) 2015 Krokodev
// Spreadbot.Tests.Nunit.Ebay
// Ebay_Configuration_Tests.cs

using NUnit.Framework;
using Spreadbot.Core.Channels.Ebay.Configuration.Sections;
using Spreadbot.Core.Channels.Ebay.Configuration.Settings;
using Spreadbot.Nunit.Ebay.Base;

namespace Spreadbot.Nunit.Ebay.Tests
{
    [TestFixture]
    public class Ebay_Configuration_Tests : Ebay_Tests
    {
        [Test]
        public void Read_Ebay_Config()
        {
            var configuration = EbayPublicConfig.Instance;
            Assert.AreEqual( "mip.ebay.com", configuration.MipConnection.HostName );
            Assert.AreEqual( 22, configuration.MipConnection.PortNumber );
        }

        [Test]
        public void Read_Ebay_Secret_Config()
        {
            var configuration = EbaySecretConfig.Instance;
            Assert.AreEqual( "cyfir",
                configuration.MipSecretData.UserName,
                @"Please edit records in [App_data\Configs\Ebay.Secret.config]" );
        }

        [Test]
        public void Mip_TimeZone()
        {
            Assert.That( EbaySettings.TimeZone, Is.EqualTo( "Mountain Standard Time" ) );
        }
    }
}