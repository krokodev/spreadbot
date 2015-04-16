// Spreadbot (c) 2015 Krokodev
// Spreadbot.Tests.Nunit.Ebay
// MipConfiguration_Tests.cs

using NUnit.Framework;
using Spreadbot.Core.Channels.Ebay.Configuration.Sections;
using Spreadbot.Core.Channels.Ebay.Mip.Settings;
using Spreadbot.Nunit.Ebay.Base;

namespace Spreadbot.Nunit.Ebay.Tests
{
    [TestFixture]
    public class MipConfigurationEbayTests : SpreadbotEbayTestBase
    {
        [Test]
        public void Read_Mip_Config()
        {
            var configuration = MipPublicConfig.Instance;
            Assert.AreEqual( "mip.ebay.com", configuration.MipConnection.HostName );
            Assert.AreEqual( 22, configuration.MipConnection.PortNumber );
        }

        [Test]
        public void Read_Mip_Security_Config()
        {
            var configuration = MipSecurityConfig.Instance;
            Assert.AreEqual( "cyfir", configuration.MipSecretData.UserName );
        }

        [Test]
        public void Mip_TimeZone()
        {
            Assert.That( MipSettings.TimeZone, Is.EqualTo( "Mountain Standard Time" ) );
        }
    }
}